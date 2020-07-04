using Unity.Mathematics;
using UnityEngine;

namespace UnityAnimatables
{
    [ExecuteInEditMode]
    public class GridSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] GameObject prototype = null;

        [Header("Grid")]
        [SerializeField] bool randomizeSeed = true;
        [SerializeField] int seed = 42;
        [SerializeField] int countx = 8;
        [SerializeField] int countz = 5;
        [SerializeField] Vector2 gridSize = new Vector3(2f, 1.2f);
        
        [Header("Randomize")]
        [SerializeField] Vector3 randPosition = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] Vector3 positionStepSize = new Vector3(0.01f, 0.01f, 0.01f);
        [SerializeField] Vector3 randRotation = new Vector3(10f, 360f, 10f);
        [SerializeField] float3 angleStepSize = new Vector3(15f, 15f, 15f);

        private void OnEnable()
        {
            gameObject.Children().ForEach(c => DestroyImmediate(c));
            seed = randomizeSeed ? UnityEngine.Random.Range(1, int.MaxValue) : seed;
            int count = countx * countz;
            Vector3[] gPositions = new Vector3[count];
            Quaternion[] rRotations = new Vector3[count].RandomRange(seed, -randRotation, randRotation)
            .ToNearest(angleStepSize).ToQuaternion();

            Vector2 increment = gridSize / new Vector2(countx, countz);
            Vector2 offset = (gridSize + increment) * 0.5f;
            int ind = 0;
            for (int i = 0; i < countx; i++)
            {
                for (int j = 0; j < countz; j++)
                {
                    gPositions[ind++] = new Vector3(
                        ((i + 1) * increment.x) - offset.x,
                        0f,
                        ((j + 1) * increment.y) - offset.y
                    );
                }
            }

            Vector3 p = transform.position;
            Vector3[] rPositions = new Vector3[count].RandomRange(seed, -randPosition, randPosition)
            .ToNearest(positionStepSize);
            for (int i = 0; i < count; i++)
            {
                var o = Instantiate(prototype, p + gPositions[i] + rPositions[i], rRotations[i], transform);
                o.name = prototype.name + i;
            }
        }

        private void OnDisable()
        {
            gameObject.Children().ForEach(c => DestroyImmediate(c));
        }
    }
}

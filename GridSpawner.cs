using Unity.Mathematics;
using UnityEngine;

namespace UnityAnimatables
{
    [ExecuteInEditMode]
    public class GridSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] GameObject pyramid = null;

        [Header("Grid")]
        [SerializeField] bool randomizeSeed = true;
        [SerializeField] int seed = 42;
        [SerializeField] int countx = 8;
        [SerializeField] int countz = 5;
        [SerializeField] Vector2 gridSize = new Vector3(2f, 1.2f);
        [Header("Randomize")]
        [SerializeField] Vector3 randPosition = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] Vector3 randRotation = new Vector3(10f, 360f, 10f);
        [SerializeField] float3 angleStepSize = new Vector3(15f, 15f, 15f);

        private void OnEnable()
        {
            gameObject.Children().ForEach(c => DestroyImmediate(c));
            seed = randomizeSeed ? UnityEngine.Random.Range(1, int.MaxValue) : seed;
            int count = countx * countz;
            Vector3[] gPositions = new Vector3[count];
            Quaternion[] rRotations = new Quaternion[count].RandomAngleStep(seed, angleStepSize).MultiplyEuler(randRotation / 360f);

            Vector3 increments = new Vector3(gridSize.x / countx, 0f, gridSize.y / countz);
            Vector3 offset = new Vector3(gridSize.x - (gridSize.x * 0.5f), 0f, gridSize.y - (gridSize.y * 0.5f));
            int ind = 0;
            for (int i = 0; i < countx; i++)
            {
                for (int j = 0; j < countz; j++)
                {
                    gPositions[ind++] = new Vector3(((i + 1) * increments.x) - offset.x - (0.5f * increments.x),
                    0f,
                    ((j + 1) * increments.z) - offset.z - (0.5f * increments.z));
                }
            }

            Vector3[] rPositions = new Vector3[count].Randomize(seed).Multiply(randPosition);
            for (int i = 0; i < count; i++)
            {
                var o = Instantiate(pyramid, gPositions[i] + rPositions[i] + transform.position, rRotations[i], transform);
                o.name = i.ToString();
            }
        }

        private void OnDisable()
        {
            gameObject.Children().ForEach(c => DestroyImmediate(c));
        }
    }
}

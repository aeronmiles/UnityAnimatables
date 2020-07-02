using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class Animator : MonoBehaviour
{
    public List<Animatable> Animatables = new List<Animatable>();
    public static Animator I { get; private set; }

    private void OnEnable()
    {
        I = this;
    }

    private void Update()
    {
        int l = Animatables.Count;
        if (l == 0) return;

        for (int i = 0; i < l; i++)
        {
            if (Animatables[i].gameObject.activeSelf) Animatables[i].Animate();
        }
    }

    internal void Add<T>(T animatable) where T : Animatable
    {
        Animatables.Add(animatable);
    }

    internal void Remove<T>(T animatable) where T : Animatable
    {
        Animatables.Remove(animatable);
        // ToDo write dynamic array
        Animatables = Animatables.Where(x => x != null).ToList();
    }
}
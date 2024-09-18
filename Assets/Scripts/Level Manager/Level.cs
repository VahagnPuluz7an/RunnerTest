using Dreamteck.Splines;
using UnityEngine;

namespace ButchersGames
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public SplineComputer Computer { get; private set; }
    }
}
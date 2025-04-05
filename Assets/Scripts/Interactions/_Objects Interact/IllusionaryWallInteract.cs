using UnityEngine;

namespace NT
{
    public class IllusionaryWallInteract : MonoBehaviour
    {
        [Header("Illusionary Wall Settings")]
        public bool wallHasBeenHit;
        [SerializeField] float wallAlpha;
        [SerializeField] float wallFadeTimer = 2.5f;
        [SerializeField] Material wallMaterial;
        [SerializeField] Collider wallCollider;

        //  SOUND FX

        private void Start()
        {
            //  DEBUG TESTING ILLUSIONARY WALL WHEN WE START, ILLUSIONARY WALL ALWAYS ACTIVE
            wallMaterial.color = new Color(1, 1, 1, 1);
        }

        private void Update()
        {
            if (wallHasBeenHit)
                WallFadingAfterHasBeenHit();
        }

        public void WallFadingAfterHasBeenHit()
        {
            wallAlpha = wallMaterial.color.a;
            wallAlpha -= Time.deltaTime / wallFadeTimer;
            Color wallColor = new Color(1, 1, 1, wallAlpha);
            wallMaterial.color = wallColor;

            if (wallCollider.enabled)
                wallCollider.enabled = false;

            if (wallAlpha <= 0)
                Destroy(this);
        }
    }
}
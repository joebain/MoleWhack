using UnityEngine;
using System.Collections;

namespace MoleBash
{
    public class MoleController : MonoBehaviour
    {

        private Animator animator;
        private new Collider collider;
        private ParticleSystem hitParticles;

        private bool Appear = false;

        [SerializeField]
        private float Duration;

        public bool Hidden {
            get
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                return !animator.IsInTransition(0) && stateInfo.IsName("Hidden");
            }
        }

        private bool hit = false;
        private bool canHit = true;

        public bool Hit { get { bool wasHit = hit; hit = false; return wasHit; } set { hit = value; } }

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            collider = GetComponent<Collider>();
            hitParticles = GetComponentInChildren<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!animator.IsInTransition(0))
            {
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (!stateInfo.IsName("Hidden"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (canHit && Input.GetMouseButtonDown(0))
                    {
                        if (collider.Raycast(ray, out hit, 1000f))
                        {
                            animator.SetTrigger("Hit");
                            hitParticles.Play();
                            this.hit = true;
                            canHit = false;
                        }
                    }

                    Duration -= Time.deltaTime;
                    if (!this.hit && Duration <= 0)
                    {
                        animator.SetTrigger("Hide");
                    }
                }
                else
                {
                    canHit = true;
                }


                if (stateInfo.IsName("Hidden"))
                {
                    if (Appear)
                    {
                        Appear = false;
                        animator.SetTrigger("Appear");
                    }
                }
                else if (stateInfo.IsName("Idle"))
                {
                    if (Duration > 0) {
                        int rand = Mathf.FloorToInt(Random.Range(0, 3));
                        if (rand == 0)
                        {
                            animator.SetTrigger("Look");
                        }
                        else if (rand == 1)
                        {
                            animator.SetTrigger("Wave");
                        }
                        else if (rand == 2)
                        {
                            animator.SetTrigger("Scratch");
                        }
                    }
                }
            }
        }

        public void Show(float duration)
        {
            Debug.Log("show mole " + gameObject.name + " with duration " + duration);
            Appear = true;
            Duration = duration;
        }

        public void Hide()
        {
            Appear = false;
            Duration = 0;
            animator.Play("Hide");
            animator.ResetTrigger("Hide");// in case this was set
        }
    }
}

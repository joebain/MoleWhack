using UnityEngine;
using System.Collections;

namespace MoleBash
{
    public enum MoleType
    {
        Normal,
        Water,
        Fire,
        Earth,
        Ice
    }

    public class MoleController : MonoBehaviour
    {

        private Animator animator;
        private new Collider collider;
        private ParticleSystem hitParticles;

        private bool Appear = false;
        
        public MoleType Type {
            get; private set;
        }

        [SerializeField]
        private float Duration;

        public bool Hidden {
            get
            {
                if (animator == null) return true;
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                return !animator.IsInTransition(0) && stateInfo.IsName("Hidden");
            }
        }

        private bool hit = false;
        private bool canHit = true;
        private PaletteCycle palette;
        private ParticleSystem clockParticle;
        private ParticleSystem skullParticle;

        public bool Hit { get { bool wasHit = hit; hit = false; return wasHit; } set { hit = value; } }

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            collider = GetComponent<Collider>();
            hitParticles = transform.Find("HitParticles").GetComponent<ParticleSystem>();
            clockParticle = transform.Find("ClockParticle").GetComponent<ParticleSystem>();
            skullParticle = transform.Find("SkullParticle").GetComponent<ParticleSystem>();
            palette = GetComponentInChildren<PaletteCycle>();
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
                            if (Type == MoleType.Ice)
                            {
                                clockParticle.Play();
                            }
                            else if (Type == MoleType.Fire)
                            {
                                skullParticle.Play();
                            }
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
            Show(duration, MoleType.Normal);
        }

        public void Show(float duration, MoleType type)
        {
            Debug.Log("show mole " + gameObject.name + " with duration " + duration);
            Appear = true;
            Duration = duration;
            Type = type;
            palette.Palette = (int)type;
        }

        public void Hide()
        {
            hit = false;
            Appear = false;
            Duration = 0;
            if (animator != null)
            {
                animator.Play("Hide");
                animator.ResetTrigger("Hide");// in case this was set
            }
            Type = MoleType.Normal;
            if (palette != null)
            {
                palette.Palette = 0;
            }
        }
    }
}

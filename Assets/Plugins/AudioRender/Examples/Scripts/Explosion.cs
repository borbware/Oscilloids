using System.Collections.Generic;
using UnityEngine;

namespace AudioRender
{
	public class Explosion : MonoBehaviour
    {
        [SerializeField] GameObject fragment;
        [SerializeField] int fragmentCount = 10;
        [SerializeField] float force = 100.0f;
        [SerializeField] float repeatTime = 0.0f;
        [SerializeField] float lifeTime = 2.0f;

        List<GameObject> fragments = new List<GameObject>();

        public void Explode()
        {
            for (int i = 0; i < fragments.Count; ++i)
            {
                fragments[i].transform.localPosition = Vector3.zero;
                Rigidbody rigidbody = fragments[i].GetComponent<Rigidbody>();
                rigidbody.velocity = Vector3.zero;
                rigidbody.AddForce((Random.onUnitSphere) * force);
                rigidbody.AddTorque(Random.onUnitSphere * force);
            }
        }

        private void Awake()
        {
            for (int i = 0; i < fragmentCount; ++i)
            {
                GameObject fragi = Instantiate(fragment, transform);
                fragments.Add(fragi);
                Destroy(fragi, lifeTime);
            }

            Explode();
            if (repeatTime > 0.0f)
            {
                InvokeRepeating("Explode", repeatTime, repeatTime);
            } else {
                Destroy(gameObject, lifeTime);
            }
        }

        private void OnDestroy()
        {
            CancelInvoke("Explode");
        }
    }
}
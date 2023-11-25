    using System;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class RandomLight : MonoBehaviour
    {
        public float speed = 5;
        public float min = 5;
        public Gradient gradient;
        
        private Light _light;
        private float _max;
        private float _seed;

        private void Start()
        {
            _light = GetComponent<Light>();
            _max = _light.intensity;
            _seed = Random.value;
        }

        private void Update()
        {
            float t = Mathf.PingPong(_seed + (Time.time*speed), 1);
            _light.intensity = Mathf.Lerp(min, _max, t);
            _light.color = gradient.Evaluate(t);
        }
    }

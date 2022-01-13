using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        private PlayerController controls;
        private GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GetComponent<PlayableDirector>().played += DisableControl;
            controls = player.GetComponent<PlayerController>();
        }
        void DisableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrenAction();
            controls.enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            controls.enabled = true;
        }
    }
}

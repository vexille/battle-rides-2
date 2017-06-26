using LuftSchloss;
using LuftSchloss.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : LuftMonobehaviour {
    private Dictionary<string, AudioSource> _sourceDic;

    public override void Initialize() {
        base.Initialize();

        _sourceDic = new Dictionary<string, AudioSource>();

        foreach (Transform child in transform) {
            var audioSource = child.GetComponent<AudioSource>();
            if (audioSource != null) {
                _sourceDic.Add(child.name, audioSource);
            }
        }
    }

    public void PlaySound(string sound) {
        _sourceDic[sound].Play();
    }

    public void StopSound(string sound) {
        _sourceDic[sound].Stop();
    }
}

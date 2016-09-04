// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class Rotate : MonoBehaviour
{
    private Quaternion startingRotation;
    private Quaternion gvrStartingRotation;

    void Start()
    {
        startingRotation = getObjRotation();
        gvrStartingRotation = getGvrRotation();
    }

    void LateUpdate()
    {
        //GvrViewer.Instance.UpdateState();
        Quaternion currentRotation = getGvrRotation();
        float rotation = (currentRotation.x - gvrStartingRotation.x) * 100;

        SetRotation(rotation);
        resetGvrCam();

        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    private void SetRotation(float rotation)
    {
        transform.localRotation = new Quaternion(
            startingRotation.w,
            startingRotation.x,
            startingRotation.y + rotation,
            startingRotation.z
        );
    }

    private Quaternion getObjRotation()
    {
        return transform.localRotation;
    }

    private Quaternion getGvrRotation()
    {
        return GvrViewer.Controller.transform.localRotation;
    }

    private void resetGvrCam()
    {
        GvrViewer.Controller.transform.rotation = gvrStartingRotation;
    }
}
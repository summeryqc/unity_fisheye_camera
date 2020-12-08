# unity_fisheye_camera
Fisheye camera modelling by using Unity3D

[![License: MPL 2.0](https://img.shields.io/badge/License-MPL%202.0-brightgreen.svg)](https://opensource.org/licenses/MPL-2.0)

## Getting Started
This project was inspried by https://github.com/psiorx/Unity-Fisheye. Simulated a fisheye camera in Unity3D using cubemapping and a post-processing shader. A mathematic equation of a generic camera model was implemented instead of using double sphere camera model.
![undistorted_fisheye](https://github.com/summeryqc/unity_fisheye_camera/blob/main/resources/undistorted_fisheye.png)
![fisheyetest](https://github.com/summeryqc/unity_fisheye_camera/blob/main/resources/fisheyetest.png)

### Required Dependencies
**NOTE**: Right now this porject works only on windows.
1. Download and Install Unity Hub
2. Download and Install Unity 2019.2.9f1 from the [Unity Download Archive](https://unity3d.com/get-unity/download/archive)
3. `git clone https://github.com/summeryqc/unity_fisheye_camera`

## Background

To create a model for a fisheye camera, we need the following step:
1. Distortion model for the lens. Describing the mapping between incoming light angle and pixel position.
2. Imager model. Describes how the incoming light intensity is converted to a pixel value given the exposure settings. 


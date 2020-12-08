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
For many applications, lens can be treated as an ideal pinhole that simple projects all light rays through a common center of projection. However, for fisheye camera which has a very large field of view (around 180 degree), it is impossible to project the hemispherical feld of view on a finite image plane by a perspective projection.
To create a model for a fisheye camera, we need the following step:
1. Distortion model for the lens (optics). Describing the mapping between incoming light angle and pixel position.
  - Generic Camera Model: r(theta) = k_1.theta + k_2.theta^3 + k_3.theta^5 + k_4.theta^7 + k_5.theta^9  
     where r is the distance between the image point and the principal pont, theta is the angle between the principal axis and the incoming ray  
     ![lens_modelling](https://github.com/summeryqc/unity_fisheye_camera/blob/main/resources/lens_modelling.PNG)
2. Imager model (sensors). Describes how the incoming light intensity is converted to a pixel value given the exposure settings.
  - Unity3D provides a module called [Image Effect reference](https://docs.unity3d.com/540/Documentation/Manual/comp-ImageEffects.html), which can handle blooming, motion blur, depth of field or other effect.

 Shader "Custom/RenderFisheye"
 {

    Properties
    {
        _Cube ("Reflection Map", Cube) = "" {}
        _FocalLength_x ("Double Sphere Focal Length", float) = 1.0
        _FocalLength_y ("Double Sphere Focal Length", float) = 1.0
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 5.0
            
            #include "UnityCG.cginc"

            uniform samplerCUBE _Cube;
            uniform float _FocalLength_x;
            uniform float _FocalLength_y;

            struct v2f {
                float2 uv : TEXCOORD2;
                float4 pos : SV_POSITION;
            };


            v2f vert (float4 vertex : POSITION, float3 normal : NORMAL, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, uv);
                return o;
            }
            
            // Kannala Brandt Camera Model 
            // Inspired by opencv cv::fisheye::undistortPoints
            // Junhua Chang, volvo cars
            
            fixed4 frag (v2f i) : SV_Target
            {
                float cx = 0.5;
                float cy = 0.5;
                float fx = _FocalLength_x;
                float fy = _FocalLength_y;

                float k0 = -0.00271699909777283;
                float k1 = 0.0013139773637476549;
                float k2 = 0.0001019909649499696;
                float k3 = 8.738650040451831e-07;

                const float PI_2 = 3.141592653589793238462/2;

                float mx = (i.uv.x - cx) / fx;
                float my = (i.uv.y - cy) / fy;

                float ru = sqrt(mx * mx + my * my);

                // the current camera model is only valid up to 180 FOV
                // for larger FOV the loop below does not converge
                // clip values so we still get plausible results for super fisheye images > 180 grad
                ru = min(max(-PI_2, ru), PI_2);

                // what happens if not converge
                float theta = 0;

                if (ru > 1e-8)
                {
                    theta = ru;
                    const float EPS = 1e-8;
                    for (int j = 0; j < 10; j++)
                    {
                        float theta2 = theta*theta, theta4 = theta2*theta2, theta6 = theta4*theta2, theta8 = theta6*theta2;
                        float k0_theta2 = k0 * theta2, k1_theta4 = k1 * theta4, k2_theta6 = k2 * theta6, k3_theta8 = k3 * theta8;
                        /* new_theta = theta - theta_fix, theta_fix = f0(theta) / f0'(theta) */
                        float theta_fix = (theta * (1 + k0_theta2 + k1_theta4 + k2_theta6 + k3_theta8) - ru) /
                                        (1 + 3*k0_theta2 + 5*k1_theta4 + 7*k2_theta6 + 9*k3_theta8);
                        theta = theta - theta_fix;
                        if (abs(theta_fix) < EPS)
                            break;
                    }

                }
                float3 fisheye_ray = float3(sin(theta)*mx/ru, sin(theta)*my/ru, cos(theta));

                return texCUBE(_Cube, mul((float3x3)unity_CameraToWorld, normalize(fisheye_ray)));
            }
            ENDCG
        }
}
 }

Shader "Custom/URP RainShader"
{
    Properties
    {
        [HDR] _MainTex("Color (RGB) Alpha (A)", 2D) = "gray" {}
        _TintColor("Tint Color", Color) = (1, 1, 1, 1)
        _PointSpotLightMultiplier("Point/Spot Light Multiplier", Range(0, 10)) = 2
        _DirectionalLightMultiplier("Directional Light Multiplier", Range(0, 10)) = 1
        _InvFade("Soft Particles Factor", Range(0.01, 100.0)) = 1.0
        _AmbientLightMultiplier("Ambient light multiplier", Range(0, 1)) = 0.25
    }

        SubShader
        {
            Tags
            {
                "Queue" = "Overlay"
                "LightMode" = "Universal2D"
            }
            LOD 100

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha

                HLSLPROGRAM
                #pragma vertex vert
                #pragma exclude_renderers gles xbox360 ps3
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest

                #include "UnityCG.cginc"
                #include "Lighting.cginc"

                fixed4 _TintColor;
                float _DirectionalLightMultiplier;
                float _PointSpotLightMultiplier;
                float _AmbientLightMultiplier;

                #if defined(SOFTPARTICLES_ON)
                float _InvFade;
                #endif

                struct appdata
                {
                    float4 vertex : POSITION;
                    fixed4 color : COLOR;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv_MainTex : TEXCOORD0;
                    fixed4 color : COLOR0;
                    float4 pos : POSITION;
                    float4 projPos : TEXCOORD1;
                };

                float4 LightForVertex(float4 vertex)
                {
                    float3 viewPos = UnityObjectToViewPos(vertex).xyz;
                    fixed3 lightColor = UNITY_LIGHTMODEL_AMBIENT.rgb * _AmbientLightMultiplier;

                    // ここで ApplyLight の代わりに適切な処理を行う

                    return fixed4(lightColor, 1);
                }

                float4 _MainTex_ST;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                    o.color = LightForVertex(v.vertex) * v.color * _TintColor;

                    o.color *= (min(o.color.rgb, _TintColor.a).r / _TintColor.a);

                    #if defined(SOFTPARTICLES_ON)
                    o.projPos = ComputeScreenPos(o.pos);
                    COMPUTE_EYEDEPTH(o.projPos.z);
                    #endif

                    return o;
                }

                #if defined(SOFTPARTICLES_ON)
                sampler2D _CameraDepthTexture;
                #endif

                sampler2D _MainTex;

                fixed4 frag(v2f i) : COLOR
                {
                    #if defined(SOFTPARTICLES_ON)
                    float sceneZ = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
                    float partZ = i.projPos.z;
                    i.color.a *= saturate(_InvFade * (sceneZ - partZ));
                    #endif

                    return tex2D(_MainTex, i.uv_MainTex) * i.color;
                }
                ENDHLSL
            }
        }

            Fallback "Particles/Alpha Blended"
}

Shader "Custom/URPCelShaded"  
{  
    Properties  
    {  
        _Color ("Color", Color) = (1,1,1,1)  
        _MainTex ("Albedo (RGB)", 2D) = "white" {}  
        [Normal]_Normal("Normal", 2D) = "bump" {}  
        _LightCutoff("Light cutoff", Range(0,1)) = 0.5  
        _ShadowBands("Shadow bands", Range(1,4)) = 1  
  
        [Header(Specular)]  
        _SpecularMap("Specular map", 2D) = "white" {}  
        _Glossiness ("Smoothness", Range(0,1)) = 0.5  
        _SpecularColor("Specular color", Color) = (0,0,0,1)  
  
        [Header(Rim)]  
        _RimSize("Rim size", Range(0,1)) = 0  
        _RimColor("Rim color", Color) = (0,0,0,1)  
        _ShadowedRim("Rim affected by shadow", Float) = 0  
  
        [Header(Emission)]  
        _Emission("Emission", Color) = (0,0,0,1)  
    }  
    SubShader  
    {  
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalRenderPipeline" }  
        LOD 100  
  
        Pass  
        {  
            HLSLPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  
  
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"  
  
            CBUFFER_START(UnityPerMaterial)  
            float4 _Color;  
            float4 _MainTex_ST;  
            float4 _SpecularMap_ST;  
            float _LightCutoff;  
            float _ShadowBands;  
            float _Glossiness;  
            float4 _SpecularColor;  
            float _RimSize;  
            float4 _RimColor;  
            float _ShadowedRim;  
            float4 _Emission;  
            CBUFFER_END  
  
            TEXTURE2D(_MainTex);  
            SAMPLER(sampler_MainTex);  
  
            TEXTURE2D(_Normal);  
            SAMPLER(sampler_Normal);  
  
            TEXTURE2D(_SpecularMap);  
            SAMPLER(sampler_SpecularMap);  
  
            struct Attributes  
            {  
                float4 positionOS : POSITION;  
                float2 uv_MainTex : TEXCOORD0;  
                float2 uv_Normal : TEXCOORD1;  
                float2 uv_SpecularMap : TEXCOORD2;  
            };  
  
            struct Varyings  
            {  
                float2 uv_MainTex : TEXCOORD0;  
                float2 uv_Normal : TEXCOORD1;  
                float2 uv_SpecularMap : TEXCOORD2;  
                float3 worldNormal : TEXCOORD3;  
                float4 vertex : SV_POSITION;  
            };  
  
            Varyings vert(Attributes input)  
            {  
                Varyings output;  
                UNITY_SETUP_INSTANCE_ID(input);  
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);  
                output.vertex = TransformObjectToHClip(input.positionOS.xyz);  
                output.uv_MainTex = TRANSFORM_TEX(input.uv_MainTex, _MainTex);  
                output.uv_Normal = TRANSFORM_TEX(input.uv_Normal, _Normal);  
                output.uv_SpecularMap = TRANSFORM_TEX(input.uv_SpecularMap, _SpecularMap);  
                output.worldNormal = TransformObjectToWorldNormal(input.positionOS.xyz);  
                return output;  
            }  
  
            half4 frag(Varyings input) : SV_Target  
            {  
                float4 c = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv_MainTex) * _Color;  
                float3 worldNormal = UnpackNormal(SAMPLE_TEXTURE2D(_Normal, sampler_Normal, input.uv_Normal));  
                float3 viewDir = normalize(_WorldSpaceCameraPos - GetWorldSpacePosition(input.vertex).xyz);  
                float3 lightDir = GetMainLightDirection();  
  
                // Cel shading lighting  
                half nDotL = saturate(dot(worldNormal, lightDir));  
                half diff = round(saturate(nDotL / _LightCutoff) * _ShadowBands) / _ShadowBands;  
  
                float3 specular = _SpecularColor.rgb * step(1 - _Glossiness, dot(reflect(-lightDir, worldNormal), viewDir));  
  
                float rim = _RimColor.r * step(1 - _RimSize, 1 - saturate(dot(viewDir, worldNormal)));  
  
                half3 finalColor = (c.rgb + specular) * diff;  
  
                if (_ShadowedRim > 0)  
                {  
                    finalColor += rim * diff;  
                }  
                else  
                {  
                    finalColor += rim;  
                }  
  
                finalColor += c.rgb * _Emission;  
  
                return half4(finalColor, c.a);  
            }  
            ENDHLSL  
        }  
    }  
    FallBack "Hidden/Universal Render Pipeline/FallbackError"  
}
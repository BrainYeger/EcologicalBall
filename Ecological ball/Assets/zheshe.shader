// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Brain/zheshe"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
	   _Color("Color", Color) = (1,1,1,1)
		_RefractColor("Refraction Color",Color) = (1,1,1,1)
		_RefractAmount("Refraction Amount",Range(0,1)) = 0.5
		_RefractRatio("Refraction Ratio",Range(0.1,1)) = 0.5
		_CubeMap("Refraction CubeMap",Cube) = "_Skybox"{}
	   _FresnelScale("Fresnel Scale",Range(0,1)) = 0.5
		_FresnelPow("Fresnel pow",Range(1,5)) = 3
		 _Dd("dd",Range(-10,40)) = 0.01
    }
		SubShader{
			Tags { "RenderType" = "Opaque" "Queue" = "Geometry"}
			Pass
			{
				Tags{"LightMode" = "ForwardBase"}
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				#include "Lighting.cginc"
				#include "AutoLight.cginc"

				fixed4 _Color;
				fixed4 _RefractColor;
				fixed _RefractAmount;
				fixed _RefractRatio;
				fixed _FresnelScale;
				samplerCUBE _CubeMap;
				sampler2D _MainTex;
				half _FresnelPow;
				float _Dd;

				struct a2v
				{
					float4 vertex:POSITION;
					float3 normal:NORMAL;
					float2 uv:TEXCOORD0;
				};
				struct v2f
				{
					float4 pos:SV_POSITION;
					fixed3 worldNormal : TEXCOORD0;
					fixed3 refrDir : TEXCOORD1;
					float3 worldPos:TEXCOORD2;
					fixed3 worldViewDir : TEXCOORD3;
					fixed2 uv : TEXCOORD4;
					SHADOW_COORDS(4)
				};

				v2f vert(a2v v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.worldPos = mul(unity_ObjectToWorld,v.vertex);
					o.worldNormal = normalize(UnityObjectToWorldNormal(v.normal));
					o.worldViewDir = normalize(UnityWorldSpaceViewDir(o.worldPos));
					o.refrDir = refract(-o.worldViewDir,o.worldNormal,_RefractRatio);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) :SV_Target
				{
					fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
					fixed3 worldLightDir = UnityWorldSpaceLightDir(i.worldPos);
					fixed3 diffuse = _LightColor0.rgb * tex2D(_MainTex,i.uv).rgb * saturate(dot(i.worldNormal,worldLightDir));
					fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);
					fixed3 h = normalize(worldLightDir + viewDir);
					fixed gg = dot(i.worldNormal,h);
					gg = (gg*step(0.95,gg)-0.8)*5;

					fixed3 refraction = texCUBE(_CubeMap,i.refrDir).rgb * _RefractColor.rgb;
					UNITY_LIGHT_ATTENUATION(atten,i,i.worldPos);
					fixed fresnel = _FresnelScale + (1 - _FresnelScale) * pow(1 - dot(i.worldViewDir, i.worldNormal), _FresnelPow);
					fixed3 color = ambient + lerp(diffuse,refraction,fresnel) * atten + pow(saturate(gg * fixed3(1,1,1)*0.5)*0.5,_Dd);

					return fixed4(color,1.0);
				}

				ENDCG
			}
	}
	FallBack "Diffuse"
	
}

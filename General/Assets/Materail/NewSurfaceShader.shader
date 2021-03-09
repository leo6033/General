﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Toon/Basic Outline" {
	Properties{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_OutlineColor("Outline Color", Color) = (0,1,0,1)
		_Outline("Outline width", Range(0.005, 0.01)) = 0.01
		_MainTex("Texture", 2D) = "white" {}
		_BumpMap("Bumpmap", 2D) = "bump" {}
	}

		CGINCLUDE
#include "UnityCG.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : POSITION;
		float4 color : COLOR;
	};

	uniform float _Outline;
	uniform float4 _OutlineColor;

	v2f vert(appdata v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
		float2 offset = TransformViewToProjection(norm.xy);

		o.pos.xy += offset * o.pos.z * _Outline;
		o.color = _OutlineColor;
		return o;
	}
	ENDCG

		SubShader{
			Tags { "RenderType" = "Opaque" }
			UsePass "Toon/Basic/BASE"
			Pass {
				Name "OUTLINE"
				Tags { "LightMode" = "Always" }
				Cull Front
				ZWrite On
				ColorMask RGB
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				half4 frag(v2f i) :COLOR { return i.color; }
				ENDCG
			}
	}

		SubShader{
			Tags { "RenderType" = "Opaque" }
			UsePass "Toon/Basic/BASE"
			Pass {
				Name "OUTLINE"
				Tags { "LightMode" = "Always" }
				Cull Front
				ZWrite On
				ColorMask RGB
				Blend SrcAlpha OneMinusSrcAlpha

				CGPROGRAM
				#pragma vertex vert
				#pragma exclude_renderers shaderonly
				ENDCG
				SetTexture[_MainTex] { combine primary }
						}
	}
		SubShader{
			  Tags { "RenderType" = "Opaque" }
			  CGPROGRAM
			  #pragma surface surf Lambert
			  struct Input {
				float2 uv_MainTex;
				float2 uv_BumpMap;
			  };
			  sampler2D _MainTex;
			  sampler2D _BumpMap;
			  void surf(Input IN, inout SurfaceOutput o) {
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
				o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			  }
			  ENDCG
	}

		Fallback "Diffuse"
}
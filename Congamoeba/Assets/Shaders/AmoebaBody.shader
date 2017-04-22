Shader "Unlit/AmoebaBody"
{
	Properties
	{
		_Colour ("Colour", Color) = (0.5,0.5,0.5,1.0)
		_Fade ("Fade Colour", Color) = (0,0,0,1.0)
		_Amount ("Blobbiness Amount", Range(0,0.1)) = 0.025
		_Freq0 ("Blobbiness Frequency", Range(0,6)) = 3
		_Freq1 ("Blobbiness Frequency", Range(0,6)) = 5
		_Speed0 ("Blobbiness Speed", Range(-3,3)) = 1
		_Speed1 ("Blobbiness Speed", Range(-3,3)) = -1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float4 _Colour;
			float4 _Fade;
			float _Amount;
			float _Freq0;
			float _Freq1;
			float _Speed0;
			float _Speed1;
			
			v2f vert (appdata v)
			{
				float angle = v.uv.y;
				float time = _Time.y;

				float3 wsv = v.vertex;
				wsv += v.vertex * sin(angle * _Freq0 + time * _Speed0) * _Amount;
				wsv += v.vertex * sin(angle * _Freq1 + time * _Speed1) * _Amount;

				v2f o;
				o.vertex = UnityObjectToClipPos(wsv);
				o.uv = v.uv;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				return lerp (_Colour, _Fade, pow (i.uv.x, 1 / _Fade.a));
			}
			ENDCG
		}
	}
}

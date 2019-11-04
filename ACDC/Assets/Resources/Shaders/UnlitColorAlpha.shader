Shader "Joe/UnlitColorAlpha" {
    Properties{
		_Color("Color", Color) = (1,1,1,1)
		_CrossColor("Cross Section Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_PlaneNormal("PlaneNormal",Vector) = (0,-1,0,0)
		_PlanePosition("PlanePosition",Vector) = (0,0,0,1)
		_StencilMask("Stencil Mask", Range(0, 255)) = 255
	}
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		// LOD 200

		Lighting Off Cull Off ZTest LEqual ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
		Stencil
		{
			Ref [_StencilMask]
			CompBack Always
			PassBack Replace

			CompFront Always
			PassFront Zero
		}
		Cull Back
			CGPROGRAM

// #pragma surface surf Standard fullforwardshadows
#pragma surface surf Standard alpha:blend 

#pragma target 3.0

			sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;

			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _CrossColor;
		fixed3 _PlaneNormal;
		fixed3 _PlanePosition;
		bool checkVisability(fixed3 worldPos)
		{
			float dotProd1 = dot(worldPos - _PlanePosition, _PlaneNormal);
			return dotProd1 > 0  ;
		}
		void surf(Input IN, inout SurfaceOutputStandard o) {
			if (checkVisability(IN.worldPos))discard;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// o.Metallic = _Metallic;
			// o.Smoothness = _Glossiness;
			// o.Emission = _Color.rgb;
			o.Alpha = c.a;
		}

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) {
			return fixed4(0,0,0,0);//half4(s.Albedo, s.Alpha);
		}
		ENDCG
		
// 			Cull Front
// 			CGPROGRAM
// #pragma surface surf NoLighting  noambient

// 		struct Input {
// 			half2 uv_MainTex;
// 			float3 worldPos;

// 		};
// 		sampler2D _MainTex;
// 		fixed4 _Color;
// 		fixed4 _CrossColor;
// 		fixed3 _PlaneNormal;
// 		fixed3 _PlanePosition;
// 		bool checkVisability(fixed3 worldPos)
// 		{
// 			float dotProd1 = dot(worldPos - _PlanePosition, _PlaneNormal);
// 			return dotProd1 >0 ;
// 		}
// 		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
// 		{
// 			fixed4 c;
// 			c.rgb = s.Albedo;
// 			c.a = s.Alpha;
// 			return c;
// 		}

// 		void surf(Input IN, inout SurfaceOutput o)
// 		{
// 			if (checkVisability(IN.worldPos))discard;
// 			o.Albedo = _CrossColor;

// 		}
// 			ENDCG
		
	}
	FallBack "Diffuse"
}
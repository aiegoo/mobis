Shader "Unlit/Unlit UV Rotation of multiple textures in fragment"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_RotationA ("Rotation", Range(0,360)) = 0.0
        _RotatedTexA ("Texture", 2D) = "white" {}
        _RotationB ("Rotation", Range(0,360)) = 0.0
        _RotatedTexB ("Texture", 2D) = "white" {}
        
    }
    SubShader
    {
		Tags { "RenderType"="Transparent" "Queue"="Transparent"  }
        Lighting Off
        LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            // #pragma multi_compile_fog
           
            #include "UnityCG.cginc"
 
            float2 rotateUV(float2 uv, float degrees)
            {
                // rotating UV
                const float Deg2Rad = (UNITY_PI * 2.0) / 360.0;
 
               float rotationRadians = degrees * Deg2Rad; // convert degrees to radians
               float s = sin(rotationRadians); // sin and cos take radians, not degrees
               float c = cos(rotationRadians);
 
                float2x2 rotationMatrix = float2x2( c, -s, s, c); // construct simple rotation matrix
 
                uv -= 0.5; // offset UV so we rotate around 0.5 and not 0.0
                uv = mul(rotationMatrix, uv); // apply rotation matrix
                uv += 0.5; // offset UV again so UVs are in the correct location
 
                return uv;
            }
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 uv2 : TEXCOORD1; // Addition additional UV to pass
                // UNITY_FOG_COORDS(2) // changed from 1 to 2 since uv2 is using TEXCOORD1 now
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _RotatedTexA;
            float4 _RotatedTexA_ST;
            float _RotationA;
            sampler2D _RotatedTexB;
            float4 _RotatedTexB_ST;
            float _RotationB;
           
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
 
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2.xy = TRANSFORM_TEX(rotateUV(v.uv, _RotationA), _RotatedTexA);
                o.uv2.zw = TRANSFORM_TEX(rotateUV(v.uv, _RotationB), _RotatedTexB);

                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
           
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv2.xy);
 
                // sample rotated textures
                fixed4 colA = tex2D(_RotatedTexA, i.uv2.zw);
                fixed4 colB = tex2D(_RotatedTexB, i.uv2.zw);
 
                // adding the textures together just so you can see them all
                // col = (col + colA + colB) / 3.0;

				col = col * colA;
				col.a = col.a * colA.r;
 
                // apply fog
                // UNITY_APPLY_FOG(i.fogCoord, col);              
                return col;
            }

			
            ENDCG
		}

			// CGPROGRAM

			// 	#pragma surface surf Standard alpha:blend 
			// 	#pragma target 3.0
				
			// 	sampler2D _MainTex;
			// 	sampler2D _RotatedTexA;
		
			// 	struct Input {
			// 		float2 uv_MainTex;
			// 		float2 uv2_RotatedTexA;
			// 	};

			// 	void surf (Input IN, inout SurfaceOutputStandard o) {
			// 		fixed4 c1 = tex2D( _MainTex, IN.uv_MainTex );
			// 		fixed4 c2 = tex2D( _RotatedTexA, IN.uv2_RotatedTexA );

			// 		// o.Albedo = (c1.rgb * c2.rgb);
			// 		// o.Alpha = c1.a * c2.r;

			// 		//   o.Albedo = c2.rgb;
			// 		//   o.Alpha = c2.a;
			// 	}
            // ENDCG
        // }
    }
}
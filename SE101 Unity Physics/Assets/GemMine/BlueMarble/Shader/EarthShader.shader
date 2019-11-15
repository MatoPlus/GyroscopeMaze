Shader "GemMine/EarthShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("EarthTex (RGB)", 2D) = "white" {}
		_BumpMap("BumpMap", 2D) = "bump" {}
		_BumpMapIntensity("BumpMap Intensity", Range(0,1)) = 1
		_NightTex("NightTex (RGB)", 2D) = "white" {}
		_NightIntensity("Night Intensity", Range(0,1)) = 0.5
		_NightTransition("Night Transition", Range(1, 64)) = 4
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque"}
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf MyLighting

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _NightTex;
		fixed _BumpMapIntensity;
		fixed _NightIntensity;
		fixed _NightTransition;

		struct Input {
			float2 uv_MainTex;
		};

		struct SurfaceOutputStandard {
			fixed3 Albedo;      // diffuse color
            fixed3 Specular;    // specular color
            fixed3 Normal;      // tangent space normal, if written
            fixed3 Emission;
            fixed Smoothness;    // 0=rough, 1=smooth
            fixed Metallic;
            fixed Occlusion;     // occlusion (default 1)
            fixed Alpha;        // alpha for transparencies
            fixed3 Night;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		half4 LightingMyLighting(SurfaceOutputStandard s, half3 lightDir, half3 viewDir, half atten) {
			half4 c;
			fixed3 h = normalize(lightDir + viewDir);
			fixed nh = max (0,dot(s.Normal,h));
			fixed spec = pow(nh,48.0) * s.Smoothness;
			half NdotL = max(0, dot(s.Normal, lightDir)) * atten;
			fixed3 day = s.Albedo * _LightColor0.rgb * NdotL + spec * _LightColor0.rgb;
			c.rgb = lerp (s.Night, day, saturate(_NightTransition*NdotL));
			c.a = s.Alpha;
			return c;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			fixed3 n = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
			n.x *= _BumpMapIntensity;
			n.y *= _BumpMapIntensity;
			o.Normal = n;
			o.Night = tex2D (_NightTex, IN.uv_MainTex).rgb * _NightIntensity;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

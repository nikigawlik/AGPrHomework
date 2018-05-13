Shader "Custom/ShieldShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

		_MainScroll ("Main Texture Scroll", Vector) = (0,0,0,0)
		_SecTex ("Mask (RGB)", 2D) = "white" {}
		_MaskScroll ("Mask Texture Scroll", Vector) = (0,0,0,0)
		_Contrast ("Contrast Modifier", Float) = 1
		_Brightness ("Brightness Modifier", Float) = 0
		_ColorRampTex ("Color Ramp Texture", 2D) = "white" {}
		_RampStartT ("Start of Ramp Animation", Range(0,1)) = 0
		_RampEndT ("End of Ramp Animation", Range(0,1)) = 1
		_AnimationProgress ("animation progress (color and dissolve)", Range(0,1)) = 0
		_DissolveStrength ("Dissolve Strength", Float) = 1


		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Transparent"}
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:blend

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _SecTex;
		sampler2D _ColorRampTex;

		float _RampStartT;
		float _RampEndT;

		float2 _MainScroll;
		float2 _MaskScroll;

		float _Contrast;			
		float _Brightness;

		float _AnimationProgress;
		float _DissolveStrength;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SecTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex + _MainScroll * _Time);
			fixed4 c2 = tex2D (_SecTex, IN.uv_SecTex + _MaskScroll * _Time);
			float colorProgress = clamp(_AnimationProgress / (_RampEndT - _RampStartT) - _RampStartT, 0, 1);
			fixed4 color = tex2D (_ColorRampTex, float2(colorProgress, 0.0)) * _Color;
			// c = c * c2;
			c = 1.0 - (1.0-c) * (1.0-c2);
			c = c * _Contrast + _Brightness;
			c = c  - _AnimationProgress * _DissolveStrength;
			c = clamp(c, 0, 1);

			float alpha = clamp((c.r + c.g + c.b) / 3 * 3, 0, 1);

			o.Albedo = c.rgb * color;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = alpha;
			o.Emission = c.rgb * color * alpha;
		}
		ENDCG
	}
	FallBack "Diffuse"
}

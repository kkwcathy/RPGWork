Shader "Custom/CharacterShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
		_R("Red", Range(0,1)) = 0
		_G("Green", Range(0,1)) = 0
		_B("Blue", Range(0,1)) = 0

    }
    SubShader
    {
        Tags 
		{ 
			"RenderType" = "Transparent"
			"Queue" = "Transparent" 
		}
        LOD 200

        CGPROGRAM

        #pragma surface surf NoLighting

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;
		fixed _R;
		fixed _G;
		fixed _B;

        void surf (Input IN, inout SurfaceOutput o)
        {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed3 rgb = fixed3(_R, _G, _B);
			o.Albedo = c.rgb; 
			o.Emission = rgb;
			o.Alpha = c.a;
        }

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

        ENDCG
    }
    FallBack "Diffuse"
}

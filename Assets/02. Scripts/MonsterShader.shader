Shader "Custom/MonsterShader"
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
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
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



        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed3 rgb = fixed3(_R, _G, _B);
			o.Albedo = c.rgb; 
			o.Emission = rgb;
			o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

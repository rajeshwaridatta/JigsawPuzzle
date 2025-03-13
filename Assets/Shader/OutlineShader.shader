Shader "Unlit/OutlineShader"
{
    Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Float) = 3.0
    }
    SubShader
    {
        Tags { "Queue" = "Overlay" }

        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode" = "Always" }
            ZWrite On
            ZTest LEqual
            Cull Front
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha
            Offset 10, 10

            CGPROGRAM
            #pragma surface surf Lambert
            #include "UnityCG.cginc"

            struct Input
            {
                float2 uv_MainTex;
            };

            half _OutlineWidth;
            fixed4 _OutlineColor;

            void surf(Input IN, inout SurfaceOutput o)
            {
                o.Albedo = _OutlineColor.rgb;
                o.Alpha = _OutlineColor.a;
            }
            ENDCG
        }

        Pass
        {
            Name "TEXTURE"
            Tags { "LightMode" = "Always" }
            ZWrite On
            ZTest LEqual
            Cull Front
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma surface surf Lambert
            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o)
            {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG
        }
    }

    Fallback "UI/Default"
}

Shader "Custom/BlackAndWhite" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }
    SubShader {
        Tags {"Queue"="Overlay" }
        LOD 100

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;

        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            float gray = dot(c.rgb, float3(0.299, 0.587, 0.114));
            o.Albedo = gray;
            o.Alpha = c.a;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}

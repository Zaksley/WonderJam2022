Shader "Unlit/CorruptedOutline"
{
    Properties
    {
        _NoiseTexture("Noise Texture", 2D) = "" {}
        _Transparency("Transparency", Float) = 0.5
        _Speed("Speed", Float) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha
        
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

            float random (float2 uv)
            {
                return frac(sin(dot(uv,float2(12.9898,78.233)))*43758.5453123);
            }

            sampler2D _NoiseTexture;
            float _Transparency;
            float _Speed;

            v2f vert (appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                const fixed3 c1 = fixed3(0.95, 0.2, 0.2);
                const fixed3 c2 = fixed3(0.15, 0.85, 0.9);
                const float noise = tex2D(_NoiseTexture, i.uv + _Time.y * _Speed).r;
                float3 color = lerp(c1, c2, noise); 

                return fixed4(color, _Transparency);
            }
            ENDCG
        }
    }
}

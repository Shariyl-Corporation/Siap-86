Shader "Custom/TransparentBackground" {
    Properties {
        _Color ("Tint", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {"Queue"="Background" }
        LOD 100
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 vertex : SV_POSITION;
            };

            float4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}

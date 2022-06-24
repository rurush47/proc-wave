Shader "Custom/LightingShader"
{
    SubShader
    {
        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 diff : COLOR0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = v.color * nl * _LightColor0;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = i.diff;
                return col;
            }
            ENDCG
        }
    }
}
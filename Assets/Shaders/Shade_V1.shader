Shader "Unlit/Shade_V1"
{
    Properties
    {
       _Color("Color",Color) = (1,1,1,1)
       _MainTex ("Texture", 2D) = "white" {}
       _Gloss("Gloss",Float) = 1
       _numx("Numx",Float) = 1
       _numy("Numy",Float) = 1
    }
    SubShader
    {   
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            //Mesh data : vertex postion, vertex normal, UVs,tangents,vertex colours
            struct VertexInput
            {
                float4 vertex : POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normal : NORMAL;
            };
            
            struct VertexOutput
            {   
                float3 worldPos : TEXCOORD2;
                float3 normal : TEXCOORD1;
                float4 clipSpacePos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float  _Gloss;
            float _numx;
            float _numy;
            uniform float Amplitude;
            uniform int childnumber;


            //Vertex Shader Function
            VertexOutput vert (VertexInput v)
            {   
                VertexOutput o;
                o.uv0 = v.uv0;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.normal = v.normal;
                o.clipSpacePos = UnityObjectToClipPos(v.vertex);
               
                
                return o;
            }

            //value = lerp(a,b,t)
            // value = invLerp(a,b,value) - inverses inlerp i.e gives perctange of value between a and b.

            float InvLerp(float a, float b, float value)
            {
                return (value - a) / (b - a);


            }
            float3 MyLerp(float3 a, float3 b, float t)
            {
                return t * b + (1.0-t) * a;

            }

            float Posterize(float steps, float value)
            {
                return floor(value * steps) / steps;
            }


            float4 frag(VertexOutput o) : SV_Target
            {


                float2 uv = o.uv0 * 1/10;
                uv.x = uv.x + _numx * 1 / 10;
                uv.y = uv.y + _numy * 1 / 10;
                uv.x = 1 - uv.x;
                //uv = uv + Amplitude;
                //uv.x = uv.x + 1/10 * _num;
                float3 normal = normalize(o.normal); //this is interpolated, So Normals between verties mught be < 1. So we need to normalize.
                float3 colorA = float3(0.1, 0.8, 1);
                float3 colorB = float3(1, 0.1, 0.8);
                float t = uv.y;
               
                float3 blendedColors = MyLerp(colorA,colorB, Amplitude);
                float4(blendedColors,0);

                //Lighting
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.xyz;
                //Direct  diffuse lighting
                float simpleLight = max(0,dot(lightDir, normal));
                simpleLight = step(0.1,simpleLight);
                float3 difuseLight = lightColor * simpleLight;
                //Ambient Light
                float3 ambientLight = float3(0.1, 0.1, 0.1);

                //Directspecular light
                float3 camPos = _WorldSpaceCameraPos;
                float3 fragtoCam = camPos - o.worldPos;
                float3 viewDir = normalize(fragtoCam);
                float3 viewReflectVec = reflect(-viewDir, normal);
                float specularFalloff = max(0,dot(viewReflectVec, lightDir));
                specularFalloff = step(0.6, specularFalloff);
              
                //Modify Gloss
                specularFalloff = pow(specularFalloff, _Gloss);

                float3 directSpecular = specularFalloff * lightColor;
                //return float4(specularFalloff.xxx, 0);
                
                //Phong
                
                //Blinn-Phong


                //Composit
               
                float3 diffuseLight = ambientLight + difuseLight;
                float3 finalsurfacecol = diffuseLight * _MainTex_ST + directSpecular;
                //WebCamera
                float2 delta = float2(0.002, 0.002);

                float4 hr = float4(0, 0, 0, 0);
                float4 vt = float4(0, 0, 0, 0);
                hr += tex2D(_MainTex, (uv + float2(-1.0, -1.0) * delta)) * 1.0;
                hr += tex2D(_MainTex, (uv + float2(0.0, -1.0) * delta)) * 0.0;
                hr += tex2D(_MainTex, (uv + float2(1.0, -1.0) * delta)) * -1.0;
                hr += tex2D(_MainTex, (uv + float2(-1.0, 0.0) * delta)) * 2.0;
                hr += tex2D(_MainTex, (uv + float2(0.0, 0.0) * delta)) * 0.0;
                hr += tex2D(_MainTex, (uv + float2(1.0, 0.0) * delta)) * -2.0;
                hr += tex2D(_MainTex, (uv + float2(-1.0, 1.0) * delta)) * 1.0;
                hr += tex2D(_MainTex, (uv + float2(0.0, 1.0) * delta)) * 0.0;
                hr += tex2D(_MainTex, (uv + float2(1.0, 1.0) * delta)) * -1.0;

                vt += tex2D(_MainTex, (uv + float2(-1.0, -1.0) * delta)) * 1.0;
                vt += tex2D(_MainTex, (uv + float2(0.0, -1.0) * delta)) * 2.0;
                vt += tex2D(_MainTex, (uv + float2(1.0, -1.0) * delta)) * 1.0;
                vt += tex2D(_MainTex, (uv + float2(-1.0, 0.0) * delta)) * 0.0;
                vt += tex2D(_MainTex, (uv + float2(0.0, 0.0) * delta)) * 0.0;
                vt += tex2D(_MainTex, (uv + float2(1.0, 0.0) * delta)) * 0.0;
                vt += tex2D(_MainTex, (uv + float2(-1.0, 1.0) * delta)) * -1.0;
                vt += tex2D(_MainTex, (uv + float2(0.0, 1.0) * delta)) * -2.0;
                vt += tex2D(_MainTex, (uv + float2(1.0, 1.0) * delta)) * -1.0;

                hr = sqrt((hr.r * hr.r) + (hr.g * hr.g) + (hr.b * hr.b));
                vt = sqrt((vt.r * vt.r) + (vt.g * vt.g) + (vt.b * vt.b));

                float4 ColorS = float4(Amplitude/10-0.4, Amplitude/10+0.5, Amplitude + 0.3,0);

                fixed4 col = sqrt(hr * hr + vt * vt);
                //fixed4 col = tex2D(_MainTex, uv);
                return col * ColorS ;
                //return float4(finalsurfacecol,0);
            }
            ENDCG
        }
    }
}

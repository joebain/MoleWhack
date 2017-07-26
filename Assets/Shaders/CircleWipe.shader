// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,rpth:0,vtps:1,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:2,bsrc:4,bdst:1,dpts:6,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:2865,x:33112,y:33036,varname:node_2865,prsc:2|emission-4354-RGB,clip-9045-OUT;n:type:ShaderForge.SFN_Color,id:4354,x:32710,y:32921,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4354,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.427451,c2:0.6784314,c3:0.5411765,c4:1;n:type:ShaderForge.SFN_Sin,id:899,x:31913,y:33245,varname:node_899,prsc:2|IN-5552-OUT;n:type:ShaderForge.SFN_TexCoord,id:304,x:31573,y:33283,varname:node_304,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:2829,x:32118,y:33377,varname:node_2829,prsc:2|A-899-OUT,B-2254-OUT;n:type:ShaderForge.SFN_Multiply,id:5552,x:31883,y:33110,varname:node_5552,prsc:2|A-304-U,B-5174-OUT;n:type:ShaderForge.SFN_Vector1,id:2466,x:31669,y:33081,varname:node_2466,prsc:2,v1:3.141593;n:type:ShaderForge.SFN_Pi,id:5174,x:31669,y:33159,varname:node_5174,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1505,x:31753,y:33376,varname:node_1505,prsc:2|A-304-V,B-5174-OUT;n:type:ShaderForge.SFN_Sin,id:2254,x:31934,y:33400,varname:node_2254,prsc:2|IN-1505-OUT;n:type:ShaderForge.SFN_Multiply,id:3358,x:32632,y:33363,varname:node_3358,prsc:2|A-8456-OUT,B-2829-OUT;n:type:ShaderForge.SFN_Slider,id:9321,x:31883,y:33003,ptovrint:False,ptlb:wipe,ptin:_wipe,varname:node_9321,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.008547009,max:1;n:type:ShaderForge.SFN_Tan,id:8456,x:32492,y:33132,varname:node_8456,prsc:2|IN-6233-OUT;n:type:ShaderForge.SFN_Multiply,id:7093,x:32428,y:32956,varname:node_7093,prsc:2|A-9982-OUT,B-7985-OUT;n:type:ShaderForge.SFN_Vector1,id:7985,x:32258,y:33000,varname:node_7985,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:6233,x:32301,y:33132,varname:node_6233,prsc:2|A-7093-OUT,B-9773-OUT;n:type:ShaderForge.SFN_Pi,id:9982,x:32274,y:32883,varname:node_9982,prsc:2;n:type:ShaderForge.SFN_TexCoord,id:8486,x:32119,y:33629,varname:node_8486,prsc:2,uv:0;n:type:ShaderForge.SFN_ProjectionParameters,id:8794,x:32119,y:33842,varname:node_8794,prsc:2;n:type:ShaderForge.SFN_RemapRange,id:7643,x:32318,y:33629,varname:node_7643,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-8486-UVOUT;n:type:ShaderForge.SFN_Append,id:7063,x:32318,y:33801,varname:node_7063,prsc:2|A-2017-OUT,B-8794-SGN;n:type:ShaderForge.SFN_Vector1,id:2017,x:32119,y:33783,varname:node_2017,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:4497,x:32517,y:33699,varname:node_4497,prsc:2|A-7643-OUT,B-7063-OUT;n:type:ShaderForge.SFN_OneMinus,id:9045,x:32835,y:33343,varname:node_9045,prsc:2|IN-3358-OUT;n:type:ShaderForge.SFN_Power,id:9773,x:32338,y:33296,varname:node_9773,prsc:2|VAL-6704-OUT,EXP-3626-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3626,x:32098,y:33296,ptovrint:False,ptlb:power,ptin:_power,varname:node_3626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_RemapRange,id:6704,x:32112,y:33110,varname:node_6704,prsc:2,frmn:0,frmx:1,tomn:0.78,tomx:1|IN-9321-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7174,x:32804,y:33236,ptovrint:False,ptlb:opacity,ptin:_opacity,varname:node_7174,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:4354-9321-3626-7174;pass:END;sub:END;*/

Shader "Shader Forge/CircleWipe" {
    Properties {
        _Color ("Color", Color) = (0.427451,0.6784314,0.5411765,1)
        _wipe ("wipe", Range(0, 1)) = 0.008547009
        _power ("power", Float ) = 8
        _opacity ("opacity", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend DstColor Zero
            Cull Off
            ZTest Always
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _wipe;
            uniform float _power;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = v.vertex;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_5174 = 3.141592654;
                clip((1.0 - (tan(((3.141592654*0.5)*pow((_wipe*0.22+0.78),_power)))*(sin((i.uv0.r*node_5174))+sin((i.uv0.g*node_5174))))) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : SV_Target {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                o.Emission = _Color.rgb;
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}

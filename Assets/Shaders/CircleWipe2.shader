// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-7241-RGB,clip-1393-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32471,y:32812,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Sin,id:3783,x:31357,y:32990,varname:node_3783,prsc:2|IN-5763-OUT;n:type:ShaderForge.SFN_TexCoord,id:5014,x:31047,y:33021,varname:node_5014,prsc:2,uv:0;n:type:ShaderForge.SFN_Add,id:5644,x:31592,y:33115,varname:node_5644,prsc:2|A-3783-OUT,B-2343-OUT;n:type:ShaderForge.SFN_Multiply,id:5763,x:31357,y:32848,varname:node_5763,prsc:2|A-5014-U,B-6829-OUT;n:type:ShaderForge.SFN_Vector1,id:4212,x:31143,y:32819,varname:node_4212,prsc:2,v1:3.141593;n:type:ShaderForge.SFN_Pi,id:6829,x:31143,y:32897,varname:node_6829,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4261,x:31227,y:33114,varname:node_4261,prsc:2|A-5014-V,B-6829-OUT;n:type:ShaderForge.SFN_Sin,id:2343,x:31408,y:33138,varname:node_2343,prsc:2|IN-4261-OUT;n:type:ShaderForge.SFN_Multiply,id:3947,x:32106,y:33101,varname:node_3947,prsc:2|A-9754-OUT,B-5644-OUT;n:type:ShaderForge.SFN_Slider,id:7411,x:31357,y:32741,ptovrint:False,ptlb:wipe,ptin:_wipe,varname:node_9321,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7948718,max:1;n:type:ShaderForge.SFN_Tan,id:9754,x:31966,y:32870,varname:node_9754,prsc:2|IN-8081-OUT;n:type:ShaderForge.SFN_Multiply,id:6968,x:31902,y:32694,varname:node_6968,prsc:2|A-2884-OUT,B-6826-OUT;n:type:ShaderForge.SFN_Vector1,id:6826,x:31732,y:32738,varname:node_6826,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:8081,x:31775,y:32870,varname:node_8081,prsc:2|A-6968-OUT,B-5161-OUT;n:type:ShaderForge.SFN_Pi,id:2884,x:31748,y:32621,varname:node_2884,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:1393,x:32309,y:33081,varname:node_1393,prsc:2|IN-3947-OUT;n:type:ShaderForge.SFN_Power,id:5161,x:31812,y:33034,varname:node_5161,prsc:2|VAL-5816-OUT,EXP-6315-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6315,x:31572,y:33034,ptovrint:False,ptlb:power,ptin:_power,varname:node_3626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_RemapRange,id:5816,x:31586,y:32848,varname:node_5816,prsc:2,frmn:0,frmx:1,tomn:0.78,tomx:1|IN-7411-OUT;proporder:7241-7411-6315;pass:END;sub:END;*/

Shader "Shader Forge/CircleWipe2" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _wipe ("wipe", Range(0, 1)) = 0.7948718
        _power ("power", Float ) = 8
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _wipe;
            uniform float _power;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_6829 = 3.141592654;
                clip((1.0 - (tan(((3.141592654*0.5)*pow((_wipe*0.22+0.78),_power)))*(sin((i.uv0.r*node_6829))+sin((i.uv0.g*node_6829))))) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _wipe;
            uniform float _power;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_6829 = 3.141592654;
                clip((1.0 - (tan(((3.141592654*0.5)*pow((_wipe*0.22+0.78),_power)))*(sin((i.uv0.r*node_6829))+sin((i.uv0.g*node_6829))))) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

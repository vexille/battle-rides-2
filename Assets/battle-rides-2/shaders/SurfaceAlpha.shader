// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Planet/Custom Matcap Alpha" 
{
    Properties
    {
        [Toggle(VERTCOL_ON)] UseVertCol ("Vertex Color", Float) = 0
        [Toggle(MAINTEX_ON)] MainTex ("Use Texture", Float) = 0
		_MainTex ("Main Texture", 2D) = "white" {}
		 [Toggle(MATCAP_ON)] UseMatcap ("Matcap", Float) = 0
		_ToonShade1 ("Shade Default", 2D) = "grey" {}
        [Toggle(FIXEDRIM_ON)] UseFixedRim ("Fixed Rim Color", Float) = 0
        _FixedRimCol ("Fixed Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SumColor ("Sum Color", Color) = (0,0, 0, 0)
        [Toggle(FOG_ON)] UseFog ("Fog", Float) = 0
      }
   
    Subshader 
    {
    	Tags {"RenderType"="Transparent" "Queue" = "Transparent"}	
    	Blend SrcAlpha OneMinusSrcAlpha
    	ZWrite Off	
    	Cull Off
        Pass 
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"
                #pragma shader_feature VERTCOL_ON
                #pragma shader_feature FIXEDRIM_ON
                #pragma shader_feature MATCAP_ON
                #pragma shader_feature FOG_ON
                #pragma shader_feature MAINTEX_ON
				#pragma shader_feature NORMALMAP_ON
				#pragma multi_compile_fwdbase


                sampler2D _MainTex;
                float4 _MainTex_ST;
				sampler2D _ToonShade1;
				fixed4 _SumColor;
				samplerCUBE _Cube;

                #if FIXEDRIM_ON
                    fixed4 _FixedRimCol;
                #endif

                struct appdata_base0 
				{
					half4 vertex : POSITION;
					half3 normal : NORMAL;
					#if MAINTEX_ON
					half2 texcoord : TEXCOORD0;
					#endif
                    #if VERTCOL_ON
                        half4 color : COLOR;
                    #endif

				};
				
                 struct v2f 
                 {
                    half4 pos : SV_POSITION;
                    #if MAINTEX_ON
                    half2 texcoord : TEXCOORD0;
                    #endif
                    float3 texcoordn : TEXCOORD1;
                        fixed4 color : TEXCOORD3;
                    #if FOG_ON
                        UNITY_FOG_COORDS(4)
                    #endif
                    LIGHTING_COORDS(5,6)
                 };
               
                v2f vert (appdata_base0 v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
					float3 n = normalize(mul((float3x3) UNITY_MATRIX_IT_MV, v.normal).xyz);
					n.xy = n * 0.5 + 0.5;
					o.texcoordn = n;
                   	o.texcoordn.z = saturate(n.z * n.z + n.z) ;
                     #if MAINTEX_ON
					o.texcoord = (v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw);
					#endif
                    #if VERTCOL_ON
                        o.color = v.color + _SumColor;
                    #endif

                    #if FOG_ON
                        UNITY_TRANSFER_FOG(o,o.pos);
                    #endif

                    TRANSFER_VERTEX_TO_FRAGMENT(o);
                    return o;
                }

                
                fixed4 frag (v2f i) : COLOR
                {
                	fixed4 tex;

					tex = tex2D (_MainTex, i.texcoord);

					fixed3 toonShade1 = tex2D(_ToonShade1, i.texcoordn.xy);

                    tex.rgb *= toonShade1 * 2;


                    tex *= i.color;
                    tex.a = saturate(tex.a * 80 - i.color.a * 10);
                    	
                    float attenuation = LIGHT_ATTENUATION(i);
					return fixed4(tex.rgb,tex.a) * attenuation;
                }
            ENDCG
        }
    }
    //Fallback "Diffuse"
}
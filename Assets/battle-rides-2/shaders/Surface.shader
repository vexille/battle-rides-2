// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Planet/Custom Matcap" 
{
    Properties
    {
        [Toggle(VERTCOL_ON)] UseVertCol ("Vertex Color", Float) = 0
        [Toggle(MAINTEX_ON)] MainTex ("Use Texture", Float) = 0
		_MainTex ("Main Texture", 2D) = "white" {}
		 [Toggle(MATCAP_ON)] UseMatcap ("Matcap", Float) = 0
		_ToonShade1 ("Shade Default", 2D) = "grey" {}
		_Cube("Reflex", Cube) = "_Skybox" {}
        [Toggle(FIXEDRIM_ON)] UseFixedRim ("Fixed Rim Color", Float) = 0
        _FixedRimCol ("Fixed Rim Color", Color) = (1.0, 1.0, 1.0, 1.0)
		_SumColor ("Sum Color", Color) = (0,0, 0, 0)
        [Toggle(FOG_ON)] UseFog ("Fog", Float) = 0
      }
   
    Subshader 
    {
    	Tags {"RenderType"="Opaque"}
		
        Pass 
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
                #pragma glsl_no_auto_normalization
                #pragma shader_feature VERTCOL_ON
                #pragma shader_feature FIXEDRIM_ON
                #pragma shader_feature MATCAP_ON
                #pragma shader_feature FOG_ON
                #pragma shader_feature MAINTEX_ON
				#pragma shader_feature NORMALMAP_ON

                #if FOG_ON
                    #pragma multi_compile_fog
                #endif

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
                    #if VERTCOL_ON
                        fixed4 color : TEXCOORD3;
                    #endif
                    float3 refl : TEXCOORD2;
                    #if FOG_ON
                        UNITY_FOG_COORDS(4)
                    #endif
                 };
               
                v2f vert (appdata_base0 v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
					float3 n = normalize(mul((float3x3) UNITY_MATRIX_IT_MV, v.normal).xyz);
					n.xy = n * 0.5 + 0.5;
					o.texcoordn = n;
                   	o.texcoordn.z = saturate(n.z * n.z + n.z) ;
                   	float3 viewDir = WorldSpaceViewDir( v.vertex );
					float3 worldN = UnityObjectToWorldNormal( v.normal );
                     #if MAINTEX_ON
					o.texcoord = (v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw);
					#endif

					o.refl = reflect( -viewDir, worldN );
                    #if VERTCOL_ON
                        o.color = v.color + _SumColor;
                    #endif

                    #if FOG_ON
                        UNITY_TRANSFER_FOG(o,o.pos);
                    #endif

                    return o;
                }

                
                fixed4 frag (v2f i) : COLOR
                {
                	fixed4 tex;

                	#if MAINTEX_ON
					tex = tex2D (_MainTex, i.texcoord);
					#else
					tex = fixed4(1,1,1,1);
					#endif
					fixed4 refl = texCUBE( _Cube, i.refl ) * tex.a;

					fixed3 toonShade1 = tex2D(_ToonShade1, i.texcoordn.xy);

                    #if MATCAP_ON
                        #if FIXEDRIM_ON
                            tex.rgb *= lerp(_FixedRimCol.rgb * 2 ,  toonShade1 * 2, i.texcoordn.z);
                        #else
                            tex.rgb *= toonShade1 * 2;
                        #endif
                    #else
                        #if FIXEDRIM_ON
                            tex.rgb *= lerp(_FixedRimCol.rgb * 2 , 1, i.texcoordn.z);
                        #else
                            tex *= 1;
                        #endif
                    #endif



                    #if VERTCOL_ON
                        tex *= i.color;
                    #endif

                    #if FOG_ON
                        UNITY_APPLY_FOG(i.fogCoord, tex);
                    #endif

					return fixed4(tex.rgb + refl.rgb,1);
                }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
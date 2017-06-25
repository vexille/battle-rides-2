// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "BR/CubeTransparent" 
{
    Properties
    {        
		_Cube("Reflex", Cube) = "_Skybox" {}
      }
   
    Subshader 
    {
    	Tags {"RenderType"="Transparent"}	
    	Blend One One
    	ZWrite Off
        Pass 
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma fragmentoption ARB_precision_hint_fastest
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"

				samplerCUBE _Cube;

                struct appdata_base0 
				{
					half4 vertex : POSITION;
					half3 normal : NORMAL;
				};
				
                 struct v2f 
                 {
                    half4 pos : SV_POSITION;
                    float3 refl : TEXCOORD1;
                 };
               
                v2f vert (appdata_base0 v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                   	float3 viewDir = WorldSpaceViewDir( v.vertex );
					float3 worldN = UnityObjectToWorldNormal( v.normal );

					o.refl = reflect( -viewDir, worldN );
                    return o;
                }

                
                fixed4 frag (v2f i) : COLOR
                {

					fixed4 refl = texCUBE( _Cube, i.refl );

					return refl;
                }
            ENDCG
        }
    }
    //Fallback "Diffuse"
}
// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Water"
{
	Properties
	{
		_Opacity("Opacity", Range( 0 , 1)) = 0.6014619
		_Sclale_voronoi("Sclale_voronoi", Range( 0 , 10)) = 10
		_Angle("Angle", Float) = 1.37
		_Speed("Speed", Vector) = (1,1,0,0)
		_Wave("Wave", Range( 0 , 0.5)) = 0.2111562
		_Ocean_Normal("Ocean_Normal", 2D) = "bump" {}
		_Ocean_gray("Ocean_gray", 2D) = "white" {}
		_Scale("Scale", Float) = 2
		_DephDistance("Deph Distance", Float) = 0
		_Dephcolor("Dephcolor", Color) = (0,0,0,0)
		_FoamColor("FoamColor", Color) = (1,1,1,0)
		_FoamSpeed("FoamSpeed", Vector) = (-0.14,0,0,0)
		_EdgeDetection("EdgeDetection", Float) = 0.35
		_EdgeIntensty("EdgeIntensty", Float) = 1.24
		_SpeedIntensity("SpeedIntensity", Range( 0 , 1)) = 0.96
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _Ocean_gray;
		uniform float2 _Speed;
		uniform float _Scale;
		uniform float _Sclale_voronoi;
		uniform float _Angle;
		uniform float _Wave;
		uniform sampler2D _Ocean_Normal;
		uniform float _SpeedIntensity;
		uniform float2 _FoamSpeed;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _EdgeDetection;
		uniform float _EdgeIntensty;
		uniform float4 _FoamColor;
		uniform float _DephDistance;
		uniform float4 _Dephcolor;
		uniform float _Opacity;


		float2 voronoihash7( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi7( float2 v, float time, inout float2 id, inout float2 mr, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mg = 0;
			for ( int j = -3; j <= 3; j++ )
			{
				for ( int i = -3; i <= 3; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash7( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = f - g - o;
					float d = 0.707 * sqrt(dot( r, r ));
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return (F2 + F1) * 0.5;
		}


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float mulTime22 = _Time.y * 0.5;
			float2 temp_cast_0 = (_Scale).xx;
			float2 uv_TexCoord4 = v.texcoord.xy * temp_cast_0;
			float2 panner20 = ( mulTime22 * _Speed + uv_TexCoord4);
			float time7 = _Angle;
			float2 coords7 = uv_TexCoord4 * _Sclale_voronoi;
			float2 id7 = 0;
			float2 uv7 = 0;
			float fade7 = 0.5;
			float voroi7 = 0;
			float rest7 = 0;
			for( int it7 = 0; it7 <2; it7++ ){
			voroi7 += fade7 * voronoi7( coords7, time7, id7, uv7, 0 );
			rest7 += fade7;
			coords7 *= 2;
			fade7 *= 0.5;
			}//Voronoi7
			voroi7 /= rest7;
			v.vertex.xyz += ( ( tex2Dlod( _Ocean_gray, float4( panner20, 0, 0.0) ) * ( 1.0 - voroi7 ) ) * _Wave ).rgb;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float mulTime22 = _Time.y * 0.5;
			float2 temp_cast_0 = (_Scale).xx;
			float2 uv_TexCoord4 = i.uv_texcoord * temp_cast_0;
			float2 panner20 = ( mulTime22 * _Speed + uv_TexCoord4);
			o.Normal = UnpackNormal( tex2D( _Ocean_Normal, panner20 ) );
			float2 panner60 = ( _Time.y * ( _FoamSpeed * 1 ) + i.uv_texcoord);
			float simplePerlin2D62 = snoise( panner60*17.0 );
			simplePerlin2D62 = simplePerlin2D62*0.5 + 0.5;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth18 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth18 = abs( ( screenDepth18 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _EdgeDetection ) );
			float clampResult69 = clamp( ( ( 1.0 - distanceDepth18 ) * _EdgeIntensty ) , 0.0 , 1.0 );
			o.Albedo = ( step( _SpeedIntensity , ( simplePerlin2D62 + clampResult69 ) ) * _FoamColor ).rgb;
			float screenDepth39 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth39 = abs( ( screenDepth39 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DephDistance ) );
			o.Emission = ( distanceDepth39 + _Dephcolor ).rgb;
			o.Alpha = _Opacity;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float4 screenPos : TEXCOORD3;
				float4 tSpace0 : TEXCOORD4;
				float4 tSpace1 : TEXCOORD5;
				float4 tSpace2 : TEXCOORD6;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
1920;0;1920;1019;890.7169;1310.65;1.3;True;False
Node;AmplifyShaderEditor.CommentaryNode;80;-1844.594,-1200.538;Inherit;False;2223;867.7855;EDGE DETECTION;17;64;18;70;57;61;65;68;59;66;60;69;62;63;73;71;76;72;EDGE DETECTION;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-1706.692,-603.2358;Inherit;False;Property;_EdgeDetection;EdgeDetection;15;0;Create;True;0;0;0;False;0;False;0.35;0.35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;70;-1794.594,-918.7522;Inherit;False;Property;_FoamSpeed;FoamSpeed;14;0;Create;True;0;0;0;False;0;False;-0.14,0;-0.14,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DepthFade;18;-1367.31,-655.3058;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;57;-1488.64,-1150.538;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;61;-1517.324,-798.0757;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;65;-1054.594,-594.7522;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleNode;59;-1606.324,-922.0757;Inherit;False;1;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-1068.594,-448.7522;Inherit;False;Property;_EdgeIntensty;EdgeIntensty;16;0;Create;True;0;0;0;False;0;False;1.24;1.24;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;45;-2131.805,3.719349;Inherit;False;Property;_Scale;Scale;8;0;Create;True;0;0;0;False;0;False;2;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1685.091,2.219391;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;17;-1045.427,598.5372;Inherit;False;Property;_Angle;Angle;3;0;Create;True;0;0;0;False;0;False;1.37;0.43;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;22;-1462.354,353.1672;Inherit;True;1;0;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;23;-1784.354,288.1672;Inherit;True;Property;_Speed;Speed;4;0;Create;True;0;0;0;False;0;False;1,1;0,-0.25;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-773.5938,-585.7522;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1323.429,816.4416;Inherit;False;Property;_Sclale_voronoi;Sclale_voronoi;2;0;Create;True;0;0;0;False;0;False;10;10;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;60;-1185.324,-1008.076;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;62;-801.723,-964.9315;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;17;False;1;FLOAT;0
Node;AmplifyShaderEditor.VoronoiNode;7;-719.7288,612.4416;Inherit;True;2;1;1;3;2;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;3;FLOAT;0;FLOAT2;1;FLOAT2;2
Node;AmplifyShaderEditor.PannerNode;20;-1215.032,0.000246048;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;69;-568.5938,-555.7522;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-328.5938,-1146.752;Inherit;False;Property;_SpeedIntensity;SpeedIntensity;17;0;Create;True;0;0;0;False;0;False;0.96;0.96;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;63;-378.6025,-963.1118;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;38;-437.6262,325.3172;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;42;-902.7864,53.81609;Inherit;True;Property;_Ocean_gray;Ocean_gray;7;0;Create;True;0;0;0;False;0;False;-1;76e2e0a4e9134894db05c11e049dd8da;fa66a26cdeb38f940814fb3a6736f626;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;51;375.8407,-98.37964;Inherit;False;Property;_DephDistance;Deph Distance;11;0;Create;True;0;0;0;False;0;False;0;16414;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;188.0748,280.8195;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;76;-8.59375,-707.7522;Inherit;False;Property;_FoamColor;FoamColor;13;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0.03971933,0.6981132,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;56;1219.43,-620.5923;Inherit;False;Property;_Dephcolor;Dephcolor;12;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.3323692,0.4685134,0.7264151,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DepthFade;39;655.8097,-65.50708;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;570.4034,503.3493;Inherit;False;Property;_Wave;Wave;5;0;Create;True;0;0;0;False;0;False;0.2111562;0.116;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;71;-26.59375,-951.7522;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;19;-744.027,946.3371;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;476.7609,188.1071;Inherit;False;Property;_Opacity;Opacity;1;0;Create;True;0;0;0;False;0;False;0.6014619;0.835;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;862.0002,367.7454;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;1;1004.07,-997.7295;Inherit;False;Property;_Color0;Color 0;0;1;[HDR];Create;True;0;0;0;False;0;False;0,0.1321606,0.8113208,0;0.07462621,0.1254247,0.4056604,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;40;-911.7864,-176.1839;Inherit;True;Property;_Ocean_Normal;Ocean_Normal;6;0;Create;True;0;0;0;False;0;False;-1;e5224cccdd7d4324e9bcde8867a78577;47f04f6a09b14424493a48877e72d2db;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;216.4063,-927.7522;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;48;1010.421,-823.7921;Inherit;False;Property;_Color1;Color 1;10;1;[HDR];Create;True;0;0;0;False;0;False;0.1546814,0.4150943,0.346472,0;0.3207547,0.2950338,0.2950338,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;50;1281.03,-969.8937;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-529.4227,112.1443;Inherit;True;Property;_Offset;Offset;9;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-221.3985,616.0618;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT2;0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;55;1030.841,-36.37964;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1290.368,50.14148;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Water;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;18;0;64;0
WireConnection;65;0;18;0
WireConnection;59;0;70;0
WireConnection;4;0;45;0
WireConnection;66;0;65;0
WireConnection;66;1;68;0
WireConnection;60;0;57;0
WireConnection;60;2;59;0
WireConnection;60;1;61;0
WireConnection;62;0;60;0
WireConnection;7;0;4;0
WireConnection;7;1;17;0
WireConnection;7;2;8;0
WireConnection;20;0;4;0
WireConnection;20;2;23;0
WireConnection;20;1;22;0
WireConnection;69;0;66;0
WireConnection;63;0;62;0
WireConnection;63;1;69;0
WireConnection;38;0;7;0
WireConnection;42;1;20;0
WireConnection;33;0;42;0
WireConnection;33;1;38;0
WireConnection;39;0;51;0
WireConnection;71;0;73;0
WireConnection;71;1;63;0
WireConnection;30;0;33;0
WireConnection;30;1;31;0
WireConnection;40;1;20;0
WireConnection;72;0;71;0
WireConnection;72;1;76;0
WireConnection;50;0;1;0
WireConnection;50;1;48;0
WireConnection;9;0;7;0
WireConnection;9;1;4;0
WireConnection;9;2;19;0
WireConnection;55;0;39;0
WireConnection;55;1;56;0
WireConnection;0;0;72;0
WireConnection;0;1;40;0
WireConnection;0;2;55;0
WireConnection;0;9;14;0
WireConnection;0;11;30;0
ASEEND*/
//CHKSM=F0AF34A2F9F16D041F2ED796C5AD4834C63D3B32
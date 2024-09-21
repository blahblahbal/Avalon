sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float4 uShaderSpecificData;

float4 AddColor(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    
	float4 color = tex2D(uImage0, coords);
	float originalAlpha = color.a;
	float3 newColor = float3(color.r * color.r + uColor.r, color.b * color.b + uColor.b, color.g * color.g + uColor.g);
	color.rbg = lerp(color.rbg, newColor, uOpacity * uSaturation);
	return color * originalAlpha;
    
}

technique Technique1
{
	pass AdditiveColor
	{
		PixelShader = compile ps_2_0 AddColor();
	}
}
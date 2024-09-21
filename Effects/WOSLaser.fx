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

float4 Laser(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
	float4 color = tex2D(uImage0, coords);
	float4 color2 = tex2D(uImage1, float2(-cos(coords.x * 6) / 3, coords.y) + float2(uTime, 0));
	float originalAlpha = color.a;
	float3 newColor = float3(color.r * color.r - color2.r / 2 + uColor.r, color.b * color.b - color2.b / 2 + uColor.b, color.g * color.g - color2.g / 2 + uColor.g);
	color.rbg = lerp(color.rbg, newColor, uOpacity * uSaturation);
	return color * originalAlpha;
    
}

technique Technique1
{
	pass WOSLaser
	{
		PixelShader = compile ps_2_0 Laser();
	}
}
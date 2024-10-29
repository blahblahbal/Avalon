sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
float3 uColor;
float3 uSecondaryColor;
float uOpacity;
float2 uTargetPosition;
float uSaturation;
float uRotation;
float uTime;
float4 uSourceRect;
float2 uWorldPosition;
float uDirection;
float3 uLightSource;
float2 uImageSize0;
float2 uImageSize1;
float4 uLegacyArmorSourceRect;
float2 uLegacyArmorSheetSize;
    
float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    if (any(color))
    {
		float gray = dot(color.rgb, float3(0.5, 0.4, 0.3));
		
		if (gray < 0.5)
			gray *= 0.75;
		
		float2 coords2 = (((coords * uImageSize0) - (uSourceRect.xy)) / uImageSize1 * (2 + (sin(uTime * 0.5) * 0.3)));
		coords2 += uWorldPosition * 0.01 * float2(uDirection,1);
		float soulColor = tex2D(uImage1, coords2 + float2(sin(uTime + coords2.y * 0.3) * 0.3, uTime * 0.2)).
		r;
		gray += soulColor * gray * (1 + (sin(uTime + coords2.y * 0.2) * 0.5));
		
		color.rgb = lerp(uSecondaryColor, uColor, gray);
		color.rgb *= color.a; // this fixes issues with bilinear filtered pixels on the outlines of sprites
		return color * sampleColor.a;
	}
    else
    {
        return tex2D(uImage0, coords);
    }
}
    
technique Technique1
{
	pass EctoplasmalDye
	{
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}
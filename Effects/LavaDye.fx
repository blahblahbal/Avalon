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
    
    if (!any(color))
        return color;
    float2 cloudCoords2 = ((coords * uImageSize0 - uSourceRect.xy) / float2(uImageSize1.x * 0.8, uImageSize1.y)) + 0.5;
    float2 cloudCoords = ((coords * uImageSize0 - uSourceRect.xy) / float2(uImageSize1.x, uImageSize1.y) * 0.5) + 0.5;
    
    float cloud = tex2D(uImage1, cloudCoords + float2(uTime * 0.01, uTime * 0.005)) + tex2D(uImage1, cloudCoords2 + float2(uTime * 0.01, uTime * 0.005));

    float gray = dot(color.rgb, float3(0.2, 0.75, 0.15));
    color = float4(float3(gray, gray, gray), color.a);
    
    const float t0 = 0.05;
    const float t1 = 0.2;
    const float t2 = 0.3;
    const float t3 = 0.4;
    
    if (color.r > t3 || color.g > t3 || color.b > t3)
    {
        color.rgb *= float3(1, 0.6, 0) * 1.7;
    }
    else if (color.r > t2 || color.g > t2 || color.b > t2)
    {
        color.rgb *= float3(1, 0.4, 0) * 1.7;
    }
    else if (color.r > t1 || color.g > t1 || color.b > t1)
    {
        color.rgb *= float3(1, 0, 0) * 1.7;
    }
    else if (color.r > t0 || color.g > t0 || color.b > t0)
    {
        color.rgb *= float3(0, 0, 0) * 1.7;
    }
      
    if ((cloud <= 0.9 && cloud >= 0.78) || (cloud <= 0.7 && cloud >= 0.68) || (cloud <= 0.5 && cloud >= 0.48) || (cloud <= 0.3 && cloud >= 0.28) || (cloud <= 0.1 && cloud >= 0.08))
        color.rgb *= 1.9;
	
	if (sampleColor.a != 1.0) // special calculation for transparent pixels
	{
		return color * sampleColor;
	}
	return color * sampleColor.a;
}
    
technique Technique1
{
    pass LavaDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}
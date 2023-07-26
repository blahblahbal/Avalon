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

    const float t1 = 0.35;
	const float t2 = 0.42;
    const float t3 = 0.49;
    const float t4 = 0.56;
    const float t5 = 0.65;
    const float t6 = 0.7;
    float gray = dot(color.rgb, float3(0.4, 0.4, 0.4));
    if (gray > t5)
    {
        gray /= 1.2;
    }
    if (gray < 0.2)
    {
        gray *= 1.5;
    }
    color = float4(float3(gray, gray, gray), 1);

    if (color.r > t5 || color.g > t5 || color.b > t5)
    {
        color.rgb *= float3(0.6, 0.533, 0.658) * 1.1;
    }
    else if (color.r > t4 || color.g > t4 || color.b > t4)
    {
        color.rgb *= float3(0.341, 0.250, 0.427) * 1.8;
    }
    else if (color.r > t3 || color.g > t3 || color.b > t3)
    {
        color.rgb *= float3(0.376, 0.294, 0.701) * 1.1;
    }
    else if (color.r > t2 || color.g > t2 || color.b > t2)
    {
        color.rgb *= float3(0.486, 0.215, 0.592) * 1; // not run on solar armor
    }
    else if (color.r > t1 || color.g > t1 || color.b > t1)
    {
        color.rgb *= float3(0.447, 0.164, 0.4) * 1.4;
    }
    else
    {
        color.rgb *= float3(0.313, 0.113, 0.466) * 1.1;
    }

    return color * sampleColor * tex2D(uImage0, coords).a;
}
    
technique Technique1
{
    pass EbonstoneDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}
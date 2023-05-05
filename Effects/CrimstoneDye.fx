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
    
float4 ArmorNoise(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    float2 noiseCoords = (coords * uImageSize0 - uSourceRect.xy) / uImageSize1;
    float4 noise = tex2D(uImage1, noiseCoords);

    float gray = dot(noise.rgb, float3(0.4, 0.4, 0.4));
    color.rgb = float3(gray, gray, gray);




    return color * sampleColor * color.a; // Multiplying by color.a to mask the invisible pixels.
}

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

    float maxB;
    if (color.r > color.g && color.r > color.b)
    {
        maxB = color.r;
    }
    else if (color.g > color.r && color.g > color.b)
    {
        maxB = color.g;
    }
    else if (color.b > color.r && color.b > color.g)
    {
        maxB = color.b;
    }

    float gray = dot(color.rgb, float3(maxB, maxB, maxB));

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
        color.rgb *= float3(0.980, 0.325, 0.325) * 0.7;
    }
    else if (color.r > t4 || color.g > t4 || color.b > t4)
    {
        color.rgb *= float3(0.764, 0.262, 0.262) * 0.8;
    }
    else if (color.r > t2 || color.g > t2 || color.b > t2)
    {
        color.rgb *= float3(0.584, 0.188, 0.188) * 1; // not run on solar armor
    }
    else if (color.r > t1 || color.g > t1 || color.b > t1)
    {
        color.rgb *= float3(0.376, 0.121, 0.121) * 1.05;
    }
    else
    {
        color.rgb *= float3(0.239, 0.078, 0.078) * 1.2;
    }

    return color * sampleColor;
}
    
technique Technique1
{
    pass CrimstoneDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
    pass CrimstoneDyeNoise
    {
        PixelShader = compile ps_2_0 ArmorNoise();
    }
}
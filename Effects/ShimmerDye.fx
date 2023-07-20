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

float3 RGBtoHSV(float3 rgb)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float3 p = lerp(float3(rgb.z, rgb.y, K.w), float3(rgb.y, rgb.z, K.x), step(rgb.z, rgb.y));
    float3 q = lerp(float3(p.x, p.y, p.z), float3(rgb.x, p.y, K.y), step(p.x, rgb.x));
    float3 d = q - min(q, K.z);
    return float3(abs(d.z) + K.w, K.w, K.x) + float3(d.y, d.x, d.x);
}

// Convert HSV color to RGB color
float3 HSVtoRGB(float3 hsv)
{
    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    float3 p = abs(frac(hsv.xxx + K.xyz) * 6.0 - K.www);
    return hsv.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), hsv.y);
}

float4 ArmorBasic(float4 sampleColor : COLOR0, float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(uImage0, uv);
	
    if (!any(color))
        return color;
    
    // Calculate time-based offset for the rainbow
    float offset = uTime * 0.5;

    // Calculate the current band index
    int bandIndex = int(uv.y / 2) % 6;

    float brightness = dot(color.rgb, float3(0.5, 0.5, 0.5));
    
    float2 gradientCoords = (uv * uImageSize0 - uSourceRect.xy) / uLegacyArmorSheetSize;
    
    if (brightness > 0.8)
    {
        // Calculate the hue for the current band
        float hue = (gradientCoords.x + offset + float(bandIndex) / 6) - floor(gradientCoords.x + offset + float(bandIndex) / 6);

        // Calculate the color for the current pixel
        color.rgb = HSVtoRGB(float3(hue, uSaturation, brightness));

        // Sample the main texture and apply the rainbow color
        float4 texColor = tex2D(uImage0, uv);
        float4 finalColor = texColor * float4(color.rgb, uOpacity);

        finalColor.rgb = clamp(finalColor.rgb, 0.5, 1);
        
        finalColor.rgb += float3(0.3, 0, 0.4);
        
        return finalColor * sampleColor;
    }
    else
    {
        color.rgb = float3(brightness * 1.4, brightness, brightness * 2);
        color.rgb *= sin(uTime * 2 + gradientCoords.x * uDirection * 16) * 0.2 + 0.9;
        return color * sampleColor;
    }
}


    
technique Technique1
{
    pass ShimmerDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}
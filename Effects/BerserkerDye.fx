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
    
struct vertexShaderStruct
{
    float3 pos      : POSITION0;
    float2 texCoord : TEXCOORD0;
};

struct pixelShaderStruct
{
    float4 position : POSITION0;
    float2 texCoord : TEXCOORD0;
};

float maxX = 32;
float maxY = 32;

float4 ArmorBasic(float4 sampleColor : COLOR0, float2 coords : TEXCOORD0) : COLOR0
{
    float4 color = tex2D(uImage0, coords);
    
    if (!any(color))
        return color;

    float4 color2 = 0;
    float2 position = coords;

    for (int x = 0; x < 4; x++)
    {
        for (int y = 0; y < 4; y++)
        {
            color2 += tex2D(uImage0, position + float2(x, y));
        }
    }

    color2 /= 2;

    // start
    //vertexShaderStruct input;
    //pixelShaderStruct output;
    //float2 offset = float2(0.5 / maxX, 0.5 / maxY);
    //output.position = float4(input.pos, 1);
    //output.texCoord = input.texCoord + offset;
    
    //float4 color2 = tex2D(uImage0, output.texCoord);
    // end

    const float blackThreshold = 0.3;
    const float t1 = 0.33;

    const float threshold = 0.4;
    const float higherT = 0.4;
    float gray = dot(color2.rgb, float3(0.4, 0.4, 0.4));
    color = float4(float3(gray, gray, gray), 1);

    if (color.r > higherT || color.g > higherT || color.b > higherT)
    {
        color.rgb *= float3(0.29, 1, 0.984) * 1.7;
    }
    else if (color.r > threshold || color.g > threshold || color.b > threshold)
    {
        color.rgb *= float3(0.29, 1, 0.984) * 1.4;
    }
    else if (color.r > blackThreshold || color.g > blackThreshold || color.b > blackThreshold)
    {
        color.rgb *= float3(0.957, 0.266, 0.474) * 1.7;
    }
    else
    {
        color.rgb *= float3(0.957, 0.266, 0.474) * 1.2;
    }

    return color * sampleColor;
}

technique Technique1
{
    pass BerserkerDye
    {
        PixelShader = compile ps_2_0 ArmorBasic();
    }
}
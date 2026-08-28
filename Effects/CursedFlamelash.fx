matrix uTransformMatrix;
sampler2D uCloud : register(s0);
sampler2D uColors : register(s1);
float uTime;

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float3 Coord : TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float3 Coord : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(in VertexShaderInput input)
{
    VertexShaderOutput output = (VertexShaderOutput) 0;
    output.Position = mul(input.Position, uTransformMatrix);
    output.Color = input.Color;
    output.Coord = input.Coord;
    return output;
}

float4 PixelShaderFunction(in VertexShaderOutput input) : COLOR0
{
    float2 uv = float2(-(input.Coord.x + (uTime * -3)) % 1, (input.Coord.y - 0.5) / input.Coord.z + 0.5);
    float2 uv2 = float2(-(input.Coord.x + (uTime * -1)) % 1, (input.Coord.y - 0.5) / input.Coord.z + 0.5);
    uv.y += (tex2D(uCloud, uv2).r - 0.5) * 0.3;
    uv.y += (tex2D(uCloud, uv).r - 0.5) * 0.2;
    float brightness = tex2D(uCloud, uv).r * pow(sin(uv.y * 3.14), 3);
    brightness = max(brightness, pow(sin(lerp(uv.y, 0.5, -1) * 3.14), 3));
    brightness = clamp(brightness, 0, 1);
    float4 color = tex2D(uColors, float2(input.Coord.x, brightness)) * pow(sin(uv.y * 3.14), 3);
    color = lerp(color * (2 - input.Coord.x), color * tex2D(uColors, float2(input.Coord.x, brightness)).r, input.Coord.x);
    color.a *= input.Coord.x;
    return color;
}

technique Technique1
{
    pass Trail
    {
        PixelShader = compile ps_3_0 PixelShaderFunction();
        VertexShader = compile vs_3_0 VertexShaderFunction();
    }
}
//source: https://www.shadertoy.com/view/wlcGzN

float rand(float2 co)
{
    return frac(sin(dot(co.xy,float2(65.9898, 78.233))) * 25758.5497);
}

float rand11(float x)
{
    return frac(sin(x * 35.7313) * 437.5453);
}

float3 rand13(float x)
{
    float3 r = float3(0.0, 0.0, 0.0);
    r.x = rand11(x);
    r.y = rand11(r.x);
    r.z = rand11(r.z);

    float temp = r.y;
    r.z = r.y;
    r.y = temp;

    
    return r;
}

float2 rand22(float2 co)
{
    float2 res;
    res.x = rand(co);
    res.y = rand11(res.x);

    return res;
}

float rand31(float3 co)
{
    return frac(sin(dot(co,float3(65.9898,
                                 78.233, 29.3471))) * 1537.5497);
}

float3 rand33(float3 co)
{
    float r1 = rand31(co);
    float r2 = rand11(r1);
    float r3 = rand11(r2);

    return float3(r1, r2, r3);
}


float3 getPoint(float3 ijk)
{
    float3 p;
    p = rand33(ijk);

    return p;
}

float n(float3 uv)
{
    float3 ijk = floor(uv);
    float3 luv = frac(uv);

    float d = 10000.;
    float3 di = ijk;

    for(int i = -1; i <= 1; i++)
    for(int j = -1; j <= 1; j++)
    for(int k = -1; k <= 1; k++)
    {
    	float3 p = getPoint(ijk + float3(i,j,k));
        p += float3(i,j,k);
        float dist = distance(p, luv);

        if (dist < d)
        {
            d = dist;
            di = ijk + float3(i,j,k);
        }
    }
    
    return rand31(di);
}

void Voronoi3D_float(float3 pos, float scale, out float3 cells, out float3 cellsCol)
{
    float res = 0.0;;
    int steps = 1;
    float p = 1.;
    pos *= scale * 10.0;
    
    for (int i = 0; i < steps; i++)
    {
        res += n(pos * p) / p;
        
        p *= 2.;
    }

    cells = res;
    
   float3 col = rand13(res);
   cellsCol = col;

}


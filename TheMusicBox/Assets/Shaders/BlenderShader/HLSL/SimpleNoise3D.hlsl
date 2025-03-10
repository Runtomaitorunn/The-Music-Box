//source: https://www.shadertoy.com/view/sllSDl

float3 hash( float3 p , float scale)
{
	p = fmod(p, scale);
    
	p = float3( dot(p,float3(127.1,311.7, 74.7)),
			  dot(p,float3(269.5,183.3,246.1)),
			  dot(p,float3(113.5,271.9,124.6)));

	return -1.0 + 2.0*frac(sin(p)*43758.5453123);
}

void Noise3D_float(float3 pos, float scale, out float result)
{
    pos *= scale * 100;

    // grid
    float3 i = floor(pos);
    float3 w = frac(pos);
    
    #if 1
    // quintic interpolant
    float3 u = w*w*w*(w*(w*6.0-15.0)+10.0);
    float3 du = 30.0*w*w*(w*(w-2.0)+1.0);
    #else
    // cubic interpolant
    float3 u = w*w*(3.0-2.0*w);
    float3 du = 6.0*w*(1.0-w);
    #endif    
    
    // gradients
    float3 ga = hash( i+float3(0.0,0.0,0.0), scale );
    float3 gb = hash( i+float3(1.0,0.0,0.0), scale );
    float3 gc = hash( i+float3(0.0,1.0,0.0), scale );
    float3 gd = hash( i+float3(1.0,1.0,0.0), scale );
    float3 ge = hash( i+float3(0.0,0.0,1.0), scale );
	float3 gf = hash( i+float3(1.0,0.0,1.0), scale );
    float3 gg = hash( i+float3(0.0,1.0,1.0), scale );
    float3 gh = hash( i+float3(1.0,1.0,1.0), scale );
    
    // projections
    float va = dot( ga, w-float3(0.0,0.0,0.0) );
    float vb = dot( gb, w-float3(1.0,0.0,0.0) );
    float vc = dot( gc, w-float3(0.0,1.0,0.0) );
    float vd = dot( gd, w-float3(1.0,1.0,0.0) );
    float ve = dot( ge, w-float3(0.0,0.0,1.0) );
    float vf = dot( gf, w-float3(1.0,0.0,1.0) );
    float vg = dot( gg, w-float3(0.0,1.0,1.0) );
    float vh = dot( gh, w-float3(1.0,1.0,1.0) );
	
    // interpolations
    float4 noiseValue = float4( va + u.x*(vb-va) + u.y*(vc-va) + u.z*(ve-va) + u.x*u.y*(va-vb-vc+vd) + u.y*u.z*(va-vc-ve+vg) + u.z*u.x*(va-vb-ve+vf) + (-va+vb+vc-vd+ve-vf-vg+vh)*u.x*u.y*u.z,    // value
                 ga + u.x*(gb-ga) + u.y*(gc-ga) + u.z*(ge-ga) + u.x*u.y*(ga-gb-gc+gd) + u.y*u.z*(ga-gc-ge+gg) + u.z*u.x*(ga-gb-ge+gf) + (-ga+gb+gc-gd+ge-gf-gg+gh)*u.x*u.y*u.z +   // derivatives
                 du * (float3(vb,vc,ve) - va + u.yzx*float3(va-vb-vc+vd,va-vc-ve+vg,va-vb-ve+vf) + u.zxy*float3(va-vb-ve+vf,va-vb-vc+vd,va-vc-ve+vg) + u.yzx*u.zxy*(-va+vb+vc-vd+ve-vf-vg+vh) ));

   //result = step(0.1, noiseValue.x);
   result = noiseValue.x;
  
}


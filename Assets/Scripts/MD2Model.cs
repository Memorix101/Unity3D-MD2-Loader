
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MD2Model : MonoBehaviour {


	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
	[HideInInspector]
	public List<Mesh> Frames;
	[HideInInspector]
	public List<Vector3> vertex;

	public int currentFrame;
	private int nextFrame;

	public Anim currAnimation;

	[HideInInspector]
	private List<Anim> animations;

	private int lastanimation;
	public int currentAnimation;
	private int rollto_anim ;
	private bool RollOver;
	private float lastTime;
	public bool ready = false;


	float previousTimeSinceStartup;
	public void setup()
	{
		 meshFilter = (MeshFilter)    this.transform.gameObject.AddComponent(typeof(MeshFilter));
		 meshRenderer = (MeshRenderer)this.transform.gameObject.AddComponent(typeof(MeshRenderer));
		 meshRenderer.sharedMaterial = new Material(Shader.Find("Diffuse"));
		 meshRenderer.sharedMaterial.color = Color.white;
		 Frames = new List<Mesh> ();
		 vertex = new List<Vector3> ();

	}
	
	public int addAnimation(string name,int  startFrame, int endFrame, int fps)
	{
		animations.Add(new Anim(name, startFrame, endFrame, fps));
		return (animations.Count - 1);
		
	}
	public int NumAnimations()
	{
		return animations.Count - 1;
	}
	
	public void BackAnimation()
	{
		currentAnimation = (currentAnimation - 1) %  (NumAnimations());
		if (currentAnimation < 0) currentAnimation = NumAnimations();
		setAnimation(currentAnimation);
	}
	public void NextAnimation()
	{
		currentAnimation = (currentAnimation +1) %  (NumAnimations());
		if (currentAnimation >NumAnimations()) currentAnimation = 0;
		setAnimation(currentAnimation);
	}
	public void setAnimation(int num)
	{
		if (num == lastanimation) return;
		if (num > animations.Count) return;
		
		currentAnimation = num;	
		currAnimation = animations[currentAnimation];
		currentFrame = animations[currentAnimation].frameStart;
		lastanimation = currentAnimation;
	}
	public void setAnimationByName(string name)
	{
		
		for (int i=0;i<animations.Count;i++)
		{
			
			if (animations[i].name == name)
			{
				setAnimation(i);
				break;
			}
			
		}
		
	}
	public void SetAnimationRollOver(int num,int next)
	{
		if (num == lastanimation) return;
		if (num > animations.Count) return;
		
		currentAnimation = num;	
		currAnimation = animations[currentAnimation];
		currentFrame = animations[currentAnimation].frameStart;
		lastanimation = currentAnimation;
		RollOver = true;
		rollto_anim = next;
	}


	// Use this for initialization
	void Start () 
	{

		animations = new List<Anim> ();
	
		lastanimation = -1;
		currentAnimation = 0;
		rollto_anim = 0;
		RollOver=false;
		currentFrame = 0;
		lastTime = tickcount ();
	
		animations.Add(new Anim("stand", 0, 39, 9));
		animations.Add(new Anim("run", 40, 45, 10));
		animations.Add(new Anim("attack", 46, 53, 10));
		animations.Add(new Anim("pain_a", 54, 57, 7));
		animations.Add(new Anim("pain_b", 58, 61, 7));
		animations.Add(new Anim("pain_c", 62, 65, 7));
		animations.Add(new Anim("jump", 66, 71, 7));
		animations.Add(new Anim("flip", 72, 83, 7));
		animations.Add(new Anim("salute", 84, 94, 7));
		animations.Add(new Anim("fallback", 95, 111, 10));
		animations.Add(new Anim("wave", 112, 122, 7));
		animations.Add(new Anim("point", 123, 134, 6));
		animations.Add(new Anim("crouch_stand", 135, 153, 10));
		animations.Add(new Anim("crouch_walk", 154, 159, 7));
		animations.Add(new Anim("crouch_attack", 160, 168, 10));
		animations.Add(new Anim("crouch_pain", 169, 172, 7));
		animations.Add(new Anim("crouch_death", 173, 177, 5));
		animations.Add(new Anim("death_fallback", 178, 183, 7));
		animations.Add(new Anim("death_fallbackforward", 184, 189, 7));
		animations.Add(new Anim("death_fallbackslow", 190, 197, 7));
		animations.Add(new Anim("boom", 198, 198, 5));
		animations.Add(new Anim("all", 0, 198, 15));
		
		currentFrame=0;
		nextFrame = 1;

		setAnimation(0);
		ready = true;


	
	}

	private float tickcount()
	{
		return Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!ready) return;
		float time = tickcount ();
		float elapsedTime =  time - lastTime;
		//lerptime = tickcount() / (1.0f / currAnimation.fps);

	
	



		nextFrame = (currentFrame+1);
		if (nextFrame > currAnimation.frameEnd)
		{
			nextFrame = currAnimation.frameStart;
		}
		

		
		if (elapsedTime >= (1.0f / currAnimation.fps) )
		{
			currentFrame = nextFrame;
			lastTime = tickcount();	
		}

		meshFilter.sharedMesh=Frames[currentFrame];
	
	}
}

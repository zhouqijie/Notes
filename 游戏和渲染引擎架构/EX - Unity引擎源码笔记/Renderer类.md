# Renderer类    

## Renderer::Render()  

纯虚函数。在不同的子类中实现。  

## Renderers **更新** 调用层次结构      

```CPP  
=> Application::TickTimer()
    => Application::UpdateSceneIfNeed()
        => Application::UpdateScene()  
            => RenderManager::UpdateAllRenderers()
                => Renderer::UpdateAllRenderersInternal()  
                    => for(;;) renderer->Renderer::UpdateRenderer()  
                                    => if(shouldBeInScene)
                                        => if(!isInScene) GetScene().AddRenderer(this)
                                        => UpdateSceneHandle()
                                    => else 
                                        => 从场景移除    
```

## Renderer::UpdateRenderer()    

源码：  
```CPP  
if (ShouldBeInScene ())
{
	if (!IsInScene())
	{
		m_SceneHandle = GetScene().AddRenderer (this);
	}
	Assert (m_SceneHandle != kInvalidSceneHandle);
	UpdateSceneHandle ();
}
else
{
	// This should not be necessary but fixes a weird bug where a mesh is
	// being leaked when disabled before loading a scene. Happens in the fps tutorial.
	RemoveFromScene ();
}
```  


（END）  
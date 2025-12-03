
using Sandbox;

public sealed class WorkerTraceCrash : Component
{
	protected override void OnUpdate()
	{
		if ( Input.Pressed( "attack1" ) )
		{
			for ( int i = 0; i < 1000; i++ )
			{
				GameTask.RunInThreadAsync( async () =>
				{
					var colliders = this.Components.GetAll<ModelCollider>();
					Log.Info( "colliders count : " + colliders.Count() );

					foreach ( var collider in colliders )
					{
						if ( !collider.IsValid() ) { continue; }

						var results = this.GameObject.Scene.PhysicsWorld.Trace.Body( collider.KeyframeBody, this.WorldPosition ).RunAll();
					}
				});

				var colliders = this.Components.GetAll<ModelCollider>();
				colliders.ToList().ForEach( c => c.GameObject.Destroy() );
			}

		}
	}
}

<h1>hello $model.params.name$ from ST4 </h1>
	<ul>
		$model.eventTrace:{ev| 
			<li>
				$ev.message$
				<ul>
					$ev.params.keys,ev.params.values:{k,v|
						<li>$k$ = $v$</li>
					}$
				</ul>
			</li>
		}$
	</ul>
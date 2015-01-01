<h1>hello `model.params.name` from ST4 </h1>
	<ul>
		`model.eventTrace:{ev| 
			<li>
				`ev.message`
				<ul>
					`ev.params.keys:{k|
						<li>`k` = `ev.params.(k)`</li>
					}`
				</ul>
			</li>
		}`
	</ul>
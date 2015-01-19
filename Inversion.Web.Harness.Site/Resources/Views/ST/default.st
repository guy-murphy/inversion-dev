<html>
    <head>
        <title>default</title>
    </head>
    <body>
        <h2>Hello `ctx.params.name`</h2>
        <h3>context params</h3>
        <ul>
            `ctx.params.keys:{k|
            <li>`k` = `ctx.params.(k)`</li>
            }`
        </ul>

        <h3>event trace</h3>
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
    </body>
</html>
<result>
    <head>
        <title>Test 2</title>
    </head>
    <body>
        <events>
            `model.eventTrace:{ev|
            <event message="`ev.message`">
                <params `ev.params.keys:{k|
                    `k`="`ev.params.(k)`"
                }` />
            </event>
            }`
        </events>
    </body>
</result>
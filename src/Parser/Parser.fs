namespace Parser

    type JSON =
        | Number of float
        | String of string
        | Boolean of bool
        | Array of JSON list
        | Object of (string * JSON) list 
        | Null
        
        static member (?) (this, name : string) =
            match this with
            | Object xs -> xs |> List.find (fst >> (=) name)  |> snd
            | _ -> invalidOp "Expecting object"

        static member parse (str : string) =
            let idx = ref 0
            let len = str.Length
            let inline error s =
                invalidOp s

            let skipWhiteSpace () =
                while !idx < len && System.Char.IsWhiteSpace(str.[!idx]) do 
                    incr idx

            let rec parseval () =
                skipWhiteSpace ()
                if !idx < len then
                    let c = str.[!idx]
                    if c = '[' then parsearr ()
                    elif c = '{' then parseobj ()
                    elif c = '"' then parsestr ()
                    elif "-0123456789.".IndexOf(c) <> -1 then parsenum ()
                    elif c = 't' then parseLiteral("true", Boolean true)
                    elif c = 'f' then parseLiteral("false", Boolean false)
                    elif c = 'n' then parseLiteral("null", Null)
                    elif c = 'N' then parseLiteral("NaN", Number nan)
                    else error (sprintf "Unexpected tokens %s" (str.Substring(!idx)))
                else Null

            and parseLiteral (text, value) =
                if str.Substring(!idx, text.Length) = text 
                then
                    idx := !idx + text.Length
                    value
                else error "Invalid token"

            and parseobj () =
                incr idx
                skipWhiteSpace ()
                if str.[!idx] = '}' then Object []
                else
                    let pairs = parsepairs ()
                    if str.[!idx] <> '}' then error "Expecting , or }"
                    incr idx
                    Object pairs

            and parsepairs () =
                let name = 
                    match parseval () with
                    | String s -> s
                    | _ -> error "Expecting property name"

                skipWhiteSpace ()

                if str.[!idx] <> ':' then 
                    error "Expecting :"

                incr idx

                let value = parseval ()
                let pair = (name, value)

                skipWhiteSpace ()
                if str.[!idx] = ',' then
                    incr idx
                    skipWhiteSpace ()            
                    pair :: parsepairs ()
                else [pair]

            and parsearr () =
                incr idx
                skipWhiteSpace ()
                if  str.[!idx] = ']' then incr idx; Array []
                else          
                   let xs = parsevals ()
                   if str.[!idx] <> ']' then 
                    error "Expecting , or ]"
                   incr idx
                   JSON.Array xs

            and parsevals () =
                let x = parseval ()
                skipWhiteSpace ()

                if str.[!idx] = ',' then 
                   incr idx
                   skipWhiteSpace ()
                   x :: parsevals ()
                else [x]

            and parsestr () =
                incr idx
                let startIdx = !idx
                let mutable c = ' '
                let mutable isEscaped = false

                while (c <- str.[!idx]; c <> '\"') do 
                    if c = '\\' then isEscaped <- true; incr idx
                    incr idx
                let text = str.Substring(startIdx, !idx-startIdx)

                if !idx < len then 
                    incr idx

                if isEscaped then 
                    JSON.String (System.Text.RegularExpressions.Regex.Unescape text)
                else JSON.String text

            and parsenum () =
                let startIdx = !idx
                while !idx < len && "-0123456789.eE".IndexOf(str.[!idx]) <> -1 do 
                    incr idx
                let n = System.Double.Parse(str.Substring(startIdx, !idx - startIdx))
                Number n

            let value = parseval ()
            skipWhiteSpace ()

            if !idx < len then 
                error (sprintf "Unexpected tokens %s" (str.Substring(!idx)))
            value

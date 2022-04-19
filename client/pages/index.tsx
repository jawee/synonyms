import type { NextPage } from 'next'
import { useRouter } from 'next/router';

const Home: NextPage = () => {
    const router = useRouter();

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault()

        const target = event.target as typeof event.target & {
            word: { value: string };
        };

        const word = target.word.value;

        router.push(`/word/${word}`);
    }

    return (
        <>
            <form className="w-full" onSubmit={handleSubmit}>
                <div className="flex items-end mb-3">
                    <div className="relative mr-3 w-full">
                        <input title="Only a single word" pattern="[a-zA-ZÅÄÖåäö]*" required className="bg-[#2F3E46] text-[#CAD2C5] shadow appearance-none border rounded w-full py-2 px-3 mb-3 leading-tight focus:outline-none focus:shadow-outline" name="word" type="text" placeholder="Search synonyms for..." />
                    </div>
                    <div>
                        <input className="border rounded py-2 px-3 mb-3 leading-tight hover:text-[#84A9BC]" type="submit" value="Search" />
                    </div>
                </div>
            </form>
        </>
    )
}

export default Home

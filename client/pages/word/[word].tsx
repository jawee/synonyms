import type { NextPage } from 'next'
import { useRouter } from 'next/router';
import useSWR from 'swr';
import AddSynonymToWord from '../../components/add-synonym-to-word';

const Word: NextPage = () => {
  const fetcher = (url: string) => fetch(url).then((res) => res.json())
  const router = useRouter();
  const { word } = router.query;
  const { data, error } = useSWR('/api/synonym?word=' + word as string, fetcher)

  if (error) return <div>An error occurred.</div>
  if (!data) return <div>Loading...</div>
  return (
      <div className="container">
        <h1 className="text-2xl">Synonyms for {word}</h1>
        <ul>
            {data['synonyms'].map((synonym: string) => (
                <li key={synonym}>{synonym}</li>
            ))}
        </ul>

        <AddSynonymToWord word={word as string} />
      </div>
  )
}

export default Word

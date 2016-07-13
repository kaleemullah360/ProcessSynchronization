# coding: utf-8
require 'thread'
require "benchmark"
load 'animal.rb'

class Semaphore
  def initialize(size = 1)
    @queue = SizedQueue.new(size)
    size.times { inc }
  end

  def inc
    tap { @queue.push(nil) }
  end
  alias increment inc
  alias up inc

  def dec
    tap { @queue.pop }
  end
  alias decrement dec
  alias down dec

  # @return [Integer]
  def size
    @queue.length
  end

  def synchronize
    decrement
    yield
  ensure
    increment
  end
end

if $0 == __FILE__
  # Example:
  puts "Enter the file name to enter"
  STDOUT.flush
  # file_name = gets.chomp
  # puts "File name is " + file_name
  # fname = "sample.txt"
  # somefile = File.open(fname, "w")
  # ["shark","fish","seats","iteration"].each do |input|
  #   puts "Enter #{input}"
  #   shark = gets.chomp
  #   somefile.puts shark
  # end
  # somefile.close

  problem = Syncproblem.new(2,3,10,1)
	sem = Semaphore.new(2)
	threads = []
	animals = problem.fishes + problem.sharks
  # animals = animals.shuffle!
  seats = problem.table.seats
  puts "Total seats: ", seats.length
	count = (problem.fishes + problem.sharks).size
  threads = Array.new(count) do |i|
    Thread.start do
			number = (animals[i].class == Fish) ? animals[i].fish_no : animals[i].shark_no
      puts "#{animals[i].class.to_s.downcase} no #{number} trying to enter…\n"
      sem.synchronize do
        time = Benchmark.realtime do
        puts "#{animals[i].class.to_s.downcase} has entered!\n"
				 if animals[i].class.to_s.downcase == 'fish'
           # seats[i].animal_type = 'fish'
           # seats[i].occupy('fish')
					 animals[i].eat_weed(number)
				 end
				if animals[i].class.to_s.downcase == 'shark'
					animals[i].eat_weed(number)
				end
        sleep 1
        end
        puts "##{animals[i].class.to_s.downcase} no #{number} process took #{time} mm execution time \n"
      end
      sleep 1
      puts "#{animals[i].class.to_s.downcase} no #{number} has left!\n"
    end
  end
  threads.map(&:join)
	end

def time_method(method, *args)
  beginning_time = Time.now
  self.send(method, args)
  end_time = Time.now
  puts "Time elapsed #{(end_time - beginning_time)*1000} milliseconds"
end
